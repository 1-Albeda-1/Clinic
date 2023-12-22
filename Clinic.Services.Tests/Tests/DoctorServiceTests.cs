using AutoMapper;
using FluentAssertions;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Context.Tests;
using Clinic.Repositories.ReadRepositories;
using Clinic.Repositories.WriteRepositories;
using Clinic.Services.Automappers;
using Clinic.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Implementations;
using Xunit;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Tests.Tests
{
    public class DoctorServiceTests : ClinicContextInMemory
    {
        private readonly IDoctorService doctorService;
        private readonly IDoctorReadRepository doctorReadRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DoctorServiceTests"/>
        /// </summary>

        public DoctorServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            mapper = config.CreateMapper();
            doctorReadRepository = new DoctorReadRepository(Reader);
            doctorService = new DoctorService(
                doctorReadRepository,
                new DoctorWriteRepository(WriterContext),
                UnitOfWork,
                mapper
            );
        }

        /// <summary>
        /// Тест маппера
        /// </summary>
        [Fact]
        public void TestMapper()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Получение врача по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => doctorService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<Doctor>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение врача по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Doctor();
            await Context.Doctors.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Surname,
                    target.Name,
                    target.Patronymic,
                    target.CategoriesType,
                    target.DepartmentType
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Doctor}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await doctorService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Doctor}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Doctor();

            await Context.Doctors.AddRangeAsync(target,
                TestDataGenerator.Doctor(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Doctor"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => doctorService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<Doctor>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Doctor"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Doctor(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Doctors.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => doctorService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Doctor"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Doctor();
            await Context.Doctors.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => doctorService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Doctors.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Doctor"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<DoctorModel>(TestDataGenerator.Doctor());

            //Act
            Func<Task> act = () => doctorService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Doctors.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Doctor"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<DoctorModel>(TestDataGenerator.Doctor());

            //Act
            Func<Task> act = () => doctorService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<Doctor>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Doctor"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<DoctorModel>(TestDataGenerator.Doctor());
            var person = TestDataGenerator.Doctor(x => x.Id = model.Id);
            await Context.Doctors.AddAsync(person);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => doctorService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Doctors.Single(x => x.Id == person.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.CategoriesType,
                    model.DepartmentType
                });
        }
    }
}

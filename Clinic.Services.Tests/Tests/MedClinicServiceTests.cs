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
using Clinic.Tests.Extensions;

namespace Clinic.Services.Tests.Tests
{
    public class MedClinicServiceTests : ClinicContextInMemory
    {
        private readonly IMedClinicService medClinicService;
        private readonly IMedClinicReadRepository medClinicReadRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MedClinicServiceTests"/>
        /// </summary>

        public MedClinicServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            mapper = config.CreateMapper();
            medClinicReadRepository = new MedClinicReadRepository(Reader);
            medClinicService = new MedClinicService(
                medClinicReadRepository,
                new MedClinicWriteRepository(WriterContext),
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
        /// Получение поликлиники по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => medClinicService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<MedClinic>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение поликлиники по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.MedClinic();
            await Context.MedClinics.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await medClinicService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Address
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{MedClinic}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await medClinicService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{MedClinic}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.MedClinic();

            await Context.MedClinics.AddRangeAsync(target,
                TestDataGenerator.MedClinic(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await medClinicService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="MedClinic"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => medClinicService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<MedClinic>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="MedClinic"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.MedClinic(x => x.DeletedAt = DateTime.UtcNow);
            await Context.MedClinics.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => medClinicService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<MedClinic>>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="MedClinic"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.MedClinic();
            await Context.MedClinics.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => medClinicService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.MedClinics.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="MedClinic"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = mapper.Map<MedClinicModel>(TestDataGenerator.MedClinic());

            //Act
            Func<Task> act = () => medClinicService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.MedClinics.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="MedClinic"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = mapper.Map<MedClinicModel>(TestDataGenerator.MedClinic());

            //Act
            Func<Task> act = () => medClinicService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<MedClinic>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="MedClinic"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = mapper.Map<MedClinicModel>(TestDataGenerator.MedClinic());
            var person = TestDataGenerator.MedClinic(x => x.Id = model.Id);
            await Context.MedClinics.AddAsync(person);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => medClinicService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.MedClinics.Single(x => x.Id == person.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Name,
                    model.Address
                });
        }
    }
}


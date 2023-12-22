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
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.Services.Tests.Tests
{
    public class TimeTableServiceTests : ClinicContextInMemory
    {
        private readonly ITimeTableService timeTableService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableServiceTests"/>
        /// </summary>

        public TimeTableServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            mapper = config.CreateMapper();
            timeTableService = new TimeTableService(
                new TimeTableReadRepository(Reader),
                new TimeTableWriteRepository(WriterContext),
                UnitOfWork,
                new DoctorReadRepository(Reader),
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
        /// Получение расписания по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => timeTableService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<TimeTable>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTable();
            await Context.TimeTables.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Time,
                    target.Office
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{TimeTable}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await timeTableService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{TimeTable}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.TimeTable();
            await Context.TimeTables.AddRangeAsync(target,
                TestDataGenerator.TimeTable(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="TimeTable"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => timeTableService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<TimeTable>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="TimeTable"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.TimeTable(x => x.DeletedAt = DateTime.UtcNow);
            await Context.TimeTables.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => timeTableService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="TimeTable"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.TimeTable();
            await Context.TimeTables.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => timeTableService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.TimeTables.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="TimeTable"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var doctor = TestDataGenerator.Doctor();

            await Context.Doctors.AddAsync(doctor);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<TimeTableRequestModel>(TestDataGenerator.TimeTable());
            model.Doctor = doctor.Id;

            //Act
            Func<Task> act = () => timeTableService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.TimeTables.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="TimeTable"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var doctor = TestDataGenerator.Doctor();

            await Context.Doctors.AddAsync(doctor);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<TimeTableRequestModel>(TestDataGenerator.TimeTable());
            model.Doctor = doctor.Id;

            //Act
            Func<Task> act = () => timeTableService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<TimeTable>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="TimeTable"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var doctor = TestDataGenerator.Doctor();

            await Context.Doctors.AddAsync(doctor);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var timetable = TestDataGenerator.TimeTable();
            timetable.DoctorId = doctor.Id;

            var model = mapper.Map<TimeTableRequestModel>(timetable);

            await Context.TimeTables.AddAsync(timetable);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => timeTableService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.TimeTables.Single(x => x.Id == timetable.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Time,
                    model.Office
                });
        }
    }
}

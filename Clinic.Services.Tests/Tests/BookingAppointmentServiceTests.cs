using AutoMapper;
using Clinic.Context.Contracts.Models;
using Clinic.Context.Tests;
using Clinic.Repositories.ReadRepositories;
using Clinic.Repositories.WriteRepositories;
using Clinic.Services.Automappers;
using Clinic.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.ModelsRequest;
using Clinic.Services.Implementations;
using Clinic.Tests.Extensions;
using FluentAssertions;
using Xunit;

namespace Clinic.Services.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IBookingAppointmentService"/>
    /// </summary>
    public class BookingAppointmentServiceTests : ClinicContextInMemory
    {
        private readonly IBookingAppointmentService bookingAppointmentService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingAppointmentServiceTests"/>
        /// </summary>
        public BookingAppointmentServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            mapper = config.CreateMapper();

            bookingAppointmentService = new BookingAppointmentService(
                new BookingAppointmentReadRepository(Reader),
                new BookingAppointmentWriteRepository(WriterContext),
                new PatientReadRepository(Reader),
                new TimeTableReadRepository(Reader),
                UnitOfWork,
                mapper);
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
        /// Получение записи по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => bookingAppointmentService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<BookingAppointment>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.BookingAppointment();
            await Context.BookingAppointments.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Complaint
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{BookingAppointment}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await bookingAppointmentService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{BookingAppointment}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.BookingAppointment();

            await Context.BookingAppointments.AddRangeAsync(target,
                TestDataGenerator.BookingAppointment(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="BookingAppointment"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => bookingAppointmentService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<BookingAppointment>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="BookingAppointment"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.BookingAppointment(x => x.DeletedAt = DateTime.UtcNow);
            await Context.BookingAppointments.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => bookingAppointmentService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<BookingAppointment>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="BookingAppointment"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.BookingAppointment();
            await Context.BookingAppointments.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => bookingAppointmentService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.BookingAppointments.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="BookingAppointment"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var patient = TestDataGenerator.Patient();
            var timetable = TestDataGenerator.TimeTable();

            await Context.Patients.AddAsync(patient);
            await Context.TimeTables.AddAsync(timetable);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<BookingAppointmentRequestModel>(TestDataGenerator.BookingAppointment());
            model.PatientId = patient.Id;
            model.TimeTableId = timetable.Id;


            //Act
            Func<Task> act = () => bookingAppointmentService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.BookingAppointments.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="BookingAppointment"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var patient = TestDataGenerator.Patient();
            var timetable = TestDataGenerator.TimeTable();

            await Context.Patients.AddAsync(patient);
            await Context.TimeTables.AddAsync(timetable);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<BookingAppointmentRequestModel>(TestDataGenerator.BookingAppointment());
            model.PatientId = patient.Id;
            model.TimeTableId = timetable.Id;

            //Act
            Func<Task> act = () => bookingAppointmentService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<BookingAppointment>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="BookingAppointment"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var patient = TestDataGenerator.Patient();
            var timetable = TestDataGenerator.TimeTable();

            await Context.Patients.AddAsync(patient);
            await Context.TimeTables.AddAsync(timetable);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var bookingAppointment = TestDataGenerator.BookingAppointment();
            bookingAppointment.PatientId = patient.Id;
            bookingAppointment.TimeTableId = timetable.Id;

            var model = mapper.Map<BookingAppointmentRequestModel>(bookingAppointment);

            await Context.BookingAppointments.AddAsync(bookingAppointment);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => bookingAppointmentService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.BookingAppointments.Single(x => x.Id == bookingAppointment.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Complaint
                });
        }
    }
}
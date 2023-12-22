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


namespace Clinic.Services.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IBookingAppointmentService"/>
    /// </summary>
    public class BookingAppointmentServiceTests : ClinicContextInMemory
    {
        private readonly IBookingAppointmentService bookingAppointmentService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingAppointmentServiceTests"/>
        /// </summary>
        public BookingAppointmentServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            bookingAppointmentService = new BookingAppointmentService(
                new BookingAppointmentReadRepository(Reader),
                new BookingAppointmentWriteRepository(WriterContext),
                new PatientReadRepository(Reader),
                new TimeTableReadRepository(Reader),
                UnitOfWork,
                config.CreateMapper());
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
                    target.Сomplaint
                });
        }
    }
}
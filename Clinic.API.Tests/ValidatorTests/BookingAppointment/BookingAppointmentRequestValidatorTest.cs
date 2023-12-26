using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Validators.BookingAppointment;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;
using Clinic.Tests.Extensions;

namespace Clinic.API.Tests.ValidatorTests.BookingAppointment
{
    public class BookingAppointmentRequestValidatorTest : ClinicContextInMemory
    {
        private readonly BookingAppointmentRequestValidator validator;

        public BookingAppointmentRequestValidatorTest()
        {
            validator = new BookingAppointmentRequestValidator(
                new TimeTableReadRepository(Reader),
                new PatientReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = new BookingAppointmentRequest
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                TimeTableId = Guid.NewGuid(),
                Сomplaint = "a",
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldSuccess()
        {
            //Arrange
            var patient = TestDataGenerator.Patient();
            var timetable = TestDataGenerator.TimeTable();

            await Context.Patients.AddAsync(patient);
            await Context.TimeTables.AddAsync(timetable);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = new BookingAppointmentRequest
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                TimeTableId = timetable.Id,
                Сomplaint = $"Сomplaint{Guid.NewGuid():N}",
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

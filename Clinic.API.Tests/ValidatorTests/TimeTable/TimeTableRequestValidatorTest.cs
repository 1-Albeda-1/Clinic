using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Validators.TimeTable;
using Clinic.API.Validators.Patient;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;


namespace Clinic.API.Tests.ValidatorTests.TimeTable
{
    public class TimeTableRequestValidatorTest : ClinicContextInMemory
    {
        private readonly TimeTableRequestValidator validator;

        public TimeTableRequestValidatorTest()
        {
            validator = new TimeTableRequestValidator(
                new DoctorReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = new TimeTableRequest
            {
                Id = Guid.NewGuid(),
                Time = DateTimeOffset.Now,
                Office = 0,
                Doctor = Guid.NewGuid()
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
            var doctor = TestDataGenerator.Doctor();

            await Context.Doctors.AddAsync(doctor);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = new TimeTableRequest
            {
                Id = Guid.NewGuid(),
                Time = DateTimeOffset.UtcNow,
                Office = Random.Shared.Next(0, 1000),
                Doctor = doctor.Id
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

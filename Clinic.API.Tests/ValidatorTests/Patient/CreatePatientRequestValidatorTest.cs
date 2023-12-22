using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Validators.BookingAppointment;
using Clinic.API.Validators.Patient;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;

namespace Clinic.API.Tests.ValidatorTests.Patient
{
    public class CreatePatientRequestValidatorTest : ClinicContextInMemory
    {
        private readonly CreatePatientRequestValidator validator;

        public CreatePatientRequestValidatorTest()
        {
            validator = new CreatePatientRequestValidator(
                new MedClinicReadRepository(Reader),
                new DiagnosisReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = new CreatePatientRequest
            {
                Surname = "a",
                Name = "a",
                Patronymic = "a",
                Phone = "a",
                Policy = 0,
                Birthday = DateTimeOffset.Now,
                MedClinic = Guid.NewGuid(),
                Diagnosis = Guid.NewGuid()
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
            var medclinic = TestDataGenerator.MedClinic();
            var diagnosis = TestDataGenerator.Diagnosis();

            await Context.MedClinics.AddAsync(medclinic);
            await Context.Diagnosises.AddAsync(diagnosis);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = new CreatePatientRequest
            {
                Surname = $"LastName{Guid.NewGuid():N}",
                Name = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
                Policy = 1111111111111111,
                Birthday = DateTimeOffset.UtcNow,
                MedClinic = medclinic.Id,
                Diagnosis = diagnosis.Id
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

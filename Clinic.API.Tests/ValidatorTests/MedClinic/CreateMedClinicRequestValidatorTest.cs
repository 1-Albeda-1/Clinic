using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Validators.MedClinic;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;

namespace Clinic.API.Tests.ValidatorTests.MedClinic
{
    public class CreateMedClinicRequestValidatorTest 
    {
        private readonly CreateMedClinicRequestValidator validator;

        /// <summary>
        /// ctor
        /// </summary>
        public CreateMedClinicRequestValidatorTest()
        {
            validator = new CreateMedClinicRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new CreateMedClinicRequest
            {
                Name = string.Empty,
                Address = string.Empty
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = new CreateMedClinicRequest
            {
                Address = $"Address{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

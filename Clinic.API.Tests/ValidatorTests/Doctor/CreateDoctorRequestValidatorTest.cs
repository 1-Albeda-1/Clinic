using Clinic.API.Models.CreateRequest;
using Clinic.API.Validators.Doctor;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;
using Clinic.Tests.Extensions;

namespace Clinic.API.Tests.ValidatorTests.Doctor
{
    public class CreateDoctorRequestValidatorTest
    {
        private readonly CreateDoctorRequestValidator validator;

        public CreateDoctorRequestValidatorTest()
        {
            validator = new CreateDoctorRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new CreateDoctorRequest
            {
                Surname = string.Empty,
                Name = string.Empty,
                Patronymic = string.Empty,
                CategoriesType = 0,
                DepartmentType = 0
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
            var model = new CreateDoctorRequest
            {
                Surname = $"LastName{Guid.NewGuid():N}",
                Name = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                CategoriesType = Enums.CategoriesTypesResponse.None,
                DepartmentType = Enums.DepartmentTypesResponse.None,
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

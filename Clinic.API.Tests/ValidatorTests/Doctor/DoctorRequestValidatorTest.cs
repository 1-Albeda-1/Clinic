using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Validators.Doctor;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;

namespace Clinic.API.Tests.ValidatorTests.Doctor
{
    public class DoctorRequestValidatorTest
    {
        private readonly DoctorRequestValidator validator;

        public DoctorRequestValidatorTest()
        {
            validator = new DoctorRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new DoctorRequest
            {
                Id = Guid.NewGuid(),
                Surname = string.Empty,
                Name = string.Empty,
                Patronymic = string.Empty,
                CategoriesType = 100,
                DepartmentType = 100
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
            var model = new DoctorRequest
            {
                Id = Guid.NewGuid(),
                Surname = $"LastName{Guid.NewGuid():N}",
                Name = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                CategoriesType = (int)CategoriesTypes.None,
                DepartmentType = (int)DepartmentTypes.None,
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

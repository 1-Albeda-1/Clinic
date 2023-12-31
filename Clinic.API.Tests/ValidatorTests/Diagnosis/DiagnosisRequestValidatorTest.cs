﻿using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Validators.Diagnosis;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Tests;
using FluentValidation.TestHelper;
using Xunit;
using Clinic.Tests.Extensions;

namespace Clinic.API.Tests.ValidatorTests.Diagnosis
{
    public class DiagnosisRequestValidatorTest 
    {
        private readonly DiagnosisRequestValidator validator;

        public DiagnosisRequestValidatorTest()
        {
            validator = new DiagnosisRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = new DiagnosisRequest
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Medicament = string.Empty
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
            var model = new DiagnosisRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Medicament = $"Medicament{Guid.NewGuid():N}",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

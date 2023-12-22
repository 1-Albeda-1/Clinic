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

namespace Clinic.Services.Tests.Tests
{
    public class PatientServiceTests : ClinicContextInMemory
    {
        private readonly IPatientService patientService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PatientServiceTests"/>
        /// </summary>

        public PatientServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            patientService = new PatientService(
                new PatientReadRepository(Reader),
                new PatientWriteRepository(WriterContext),
                UnitOfWork,
                new DiagnosisReadRepository(Reader),
                new MedClinicReadRepository(Reader),
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение пациента по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => patientService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<Patient>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение пациента по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Patient();
            await Context.Patients.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Surname,
                    target.Name,
                    target.Patronymic,
                    target.Phone,
                    target.Policy,
                    target.Birthday
                });
        }
    }
}

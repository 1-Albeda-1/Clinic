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
using Clinic.Services.Contracts.ModelsRequest;
using Clinic.Tests.Extensions;

namespace Clinic.Services.Tests.Tests
{
    public class PatientServiceTests : ClinicContextInMemory
    {
        private readonly IPatientService patientService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PatientServiceTests"/>
        /// </summary>

        public PatientServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            mapper = config.CreateMapper();
            patientService = new PatientService(
                new PatientReadRepository(Reader),
                new PatientWriteRepository(WriterContext),
                UnitOfWork,
                new DiagnosisReadRepository(Reader),
                new MedClinicReadRepository(Reader),
                mapper
            );
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

        /// <summary>
        /// Получение <see cref="IEnumerable{Patient}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await patientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Patient}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Patient();
            await Context.Patients.AddRangeAsync(target,
                TestDataGenerator.Patient(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Patient"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => patientService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<Patient>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Patient"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Patient(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Patients.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => patientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<ClinicEntityNotFoundException<Patient>>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Patient"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Patient();
            await Context.Patients.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => patientService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Patients.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Patient"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var medclinic = TestDataGenerator.MedClinic();
            var diagnosis = TestDataGenerator.Diagnosis();

            await Context.MedClinics.AddAsync(medclinic);
            await Context.Diagnosises.AddAsync(diagnosis);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<PatientRequestModel>(TestDataGenerator.Patient());
            model.MedClinicId = medclinic.Id;
            model.DiagnosisId = diagnosis.Id;

            //Act
            Func<Task> act = () => patientService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Patients.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Patient"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var medclinic = TestDataGenerator.MedClinic();
            var diagnosis = TestDataGenerator.Diagnosis();

            await Context.MedClinics.AddAsync(medclinic);
            await Context.Diagnosises.AddAsync(diagnosis);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = mapper.Map<PatientRequestModel>(TestDataGenerator.Patient());
            model.MedClinicId = medclinic.Id;
            model.DiagnosisId = diagnosis.Id;

            //Act
            Func<Task> act = () => patientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<Patient>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Patient"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var medclinic = TestDataGenerator.MedClinic();
            var diagnosis = TestDataGenerator.Diagnosis();

            await Context.MedClinics.AddAsync(medclinic);
            await Context.Diagnosises.AddAsync(diagnosis);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var patient = TestDataGenerator.Patient();
            patient.MedClinicId = medclinic.Id;
            patient.DiagnosisId = diagnosis.Id;

            var model = mapper.Map<PatientRequestModel>(patient);

            await Context.Patients.AddAsync(patient);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => patientService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Patients.Single(x => x.Id == patient.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Phone,
                    model.Policy,
                    model.Birthday
                });
        }
    }
}

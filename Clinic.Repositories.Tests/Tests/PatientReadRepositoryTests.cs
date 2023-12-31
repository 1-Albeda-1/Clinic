﻿using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;
using Clinic.Tests.Extensions;

namespace Clinic.Repositories.Tests.Tests
{
    public class PatientReadRepositoryTests : ClinicContextInMemory
    {
        private readonly IPatientReadRepository patientReadRepository;

        public PatientReadRepositoryTests()
        {
            patientReadRepository = new PatientReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список пациентов
        /// </summary>
        [Fact]
        public async Task GetAllPatientEmpty()
        {
            // Act
            var result = await patientReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список пациентов
        /// </summary>
        [Fact]
        public async Task GetAllPatientsValue()
        {
            //Arrange
            var target = TestDataGenerator.Patient();
            await Context.Patients.AddRangeAsync(target,
                TestDataGenerator.Patient(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение пациента по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdPatientNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await patientReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение пациента по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdPatientValue()
        {
            //Arrange
            var target = TestDataGenerator.Patient();
            await Context.Patients.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка пациентов по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSPatientEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await patientReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка пациентов по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsPatientsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Patient();
            var target2 = TestDataGenerator.Patient(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Patient();
            var target4 = TestDataGenerator.Patient();
            await Context.Patients.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск пациента в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Patient();
            await Context.Patients.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск пациента в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await patientReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного пациента в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Patient(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Patients.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await patientReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}

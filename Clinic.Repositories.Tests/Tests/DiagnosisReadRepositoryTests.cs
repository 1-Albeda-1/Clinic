using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace Clinic.Repositories.Tests.Tests
{
    public class DiagnosisReadRepositoryTests : ClinicContextInMemory
    {
        private readonly IDiagnosisReadRepository diagnosisReadRepository;

        public DiagnosisReadRepositoryTests()
        {
            diagnosisReadRepository = new DiagnosisReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список диагнозов
        /// </summary>
        [Fact]
        public async Task GetAllPersonEmpty()
        {
            // Act
            var result = await diagnosisReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список диагнозов
        /// </summary>
        [Fact]
        public async Task GetAllDiagnosisesValue()
        {
            //Arrange
            var target = TestDataGenerator.Diagnosis();
            await Context.Diagnosises.AddRangeAsync(target,
                TestDataGenerator.Diagnosis(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await diagnosisReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение диагноза по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdDiagnosisNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await diagnosisReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение диагноза по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdDiagnosisValue()
        {
            //Arrange
            var target = TestDataGenerator.Diagnosis();
            await Context.Diagnosises.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await diagnosisReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка диагноза по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSDiagnosisesEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await diagnosisReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка диагнозов по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsDiagnosisesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Diagnosis();
            var target2 = TestDataGenerator.Diagnosis(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Diagnosis();
            var target4 = TestDataGenerator.Diagnosis();
            await Context.Diagnosises.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await diagnosisReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}

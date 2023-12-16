using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace Clinic.Services.Tests.Tests
{
    public class MedClinicServiceTests : ClinicContextInMemory
    {
        private readonly IMedClinicReadRepository medClinicReadRepository;

        public MedClinicServiceTests()
        {
            medClinicReadRepository = new MedClinicReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список поликлиник
        /// </summary>
        [Fact]
        public async Task GetAllMedClinicEmpty()
        {
            // Act
            var result = await medClinicReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список поликлиник
        /// </summary>
        [Fact]
        public async Task GetAllMedClinicsValue()
        {
            //Arrange
            var target = TestDataGenerator.MedClinic();
            await Context.MedClinics.AddRangeAsync(target,
                TestDataGenerator.MedClinic(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await medClinicReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение поликлиники по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdMedClinicNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await medClinicReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение поликлиники по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdMedClinicValue()
        {
            //Arrange
            var target = TestDataGenerator.MedClinic();
            await Context.MedClinics.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await medClinicReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка поликлиник по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSMedClinicEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await medClinicReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка поликлиник по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsMedClinicsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.MedClinic();
            var target2 = TestDataGenerator.MedClinic(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.MedClinic();
            var target4 = TestDataGenerator.MedClinic();
            await Context.MedClinics.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await medClinicReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}

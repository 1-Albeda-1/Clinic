using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace Clinic.Repositories.Tests.Tests
{
    public class DoctorReadRepositoryTests : ClinicContextInMemory
    {
        private readonly IDoctorReadRepository doctorReadRepository;

        public DoctorReadRepositoryTests()
        {
            doctorReadRepository = new DoctorReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список врачей
        /// </summary>
        [Fact]
        public async Task GetAllDoctorEmpty()
        {
            // Act
            var result = await doctorReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список врачей
        /// </summary>
        [Fact]
        public async Task GetAllDoctorsValue()
        {
            //Arrange
            var target = TestDataGenerator.Doctor();
            await Context.Doctors.AddRangeAsync(target,
                TestDataGenerator.Doctor(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение врача по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdDoctorNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await doctorReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение врача по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdDoctorValue()
        {
            //Arrange
            var target = TestDataGenerator.Doctor();
            await Context.Doctors.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка врачей по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSDoctorEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await doctorReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка врачей по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsDoctorsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Doctor();
            var target2 = TestDataGenerator.Doctor(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Doctor();
            var target4 = TestDataGenerator.Doctor();
            await Context.Doctors.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск врача в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Doctor();
            await Context.Doctors.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск врача в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await doctorReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного врача в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Doctor(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Doctors.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}

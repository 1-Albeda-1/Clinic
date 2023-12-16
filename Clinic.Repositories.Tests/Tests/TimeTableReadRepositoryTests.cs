using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace Clinic.Repositories.Tests.Tests
{
    public class TimeTableReadRepositoryTests : ClinicContextInMemory
    {
        private readonly ITimeTableReadRepository timeTableReadRepository;

        public TimeTableReadRepositoryTests()
        {
            timeTableReadRepository = new TimeTableReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список рассписаний
        /// </summary>
        [Fact]
        public async Task GetAllTimeTableEmpty()
        {
            // Act
            var result = await timeTableReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список рассписаний
        /// </summary>
        [Fact]
        public async Task GetAllTimeTablesValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTable();
            await Context.TimeTables.AddRangeAsync(target,
                TestDataGenerator.TimeTable(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение рассписания по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdTimeTableNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await timeTableReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение рассписания по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdTimeTableValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTable();
            await Context.TimeTables.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка рассписаний по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsSTimeTableEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await timeTableReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка рассписаний по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsTimeTablesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.TimeTable();
            var target2 = TestDataGenerator.TimeTable(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.TimeTable();
            var target4 = TestDataGenerator.TimeTable();
            await Context.TimeTables.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}

using Clinic.Context.Tests;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;
using Clinic.Tests.Extensions;

namespace Clinic.Repositories.Tests.Tests
{
    public class BookingAppointmentReadRepositoryTests : ClinicContextInMemory
    {
        private readonly IBookingAppointmentReadRepository bookingAppointmentReadRepository;

        public BookingAppointmentReadRepositoryTests()
        {
            bookingAppointmentReadRepository = new BookingAppointmentReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список записей
        /// </summary>
        [Fact]
        public async Task GetAllBookingAppointmentsEmpty()
        {

            // Act
            var result = await bookingAppointmentReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список записей
        /// </summary>
        [Fact]
        public async Task GetAllBookingAppointmentsValue()
        {
            //Arrange
            var target = TestDataGenerator.BookingAppointment();
            await Context.BookingAppointments.AddRangeAsync(target,
                TestDataGenerator.BookingAppointment(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение записи по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdBookingAppointmentNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await bookingAppointmentReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение записи по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdBookingAppointmentValue()
        {
            //Arrange
            var target = TestDataGenerator.BookingAppointment();
            await Context.BookingAppointments.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка записей по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsBookingAppointmentsEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await bookingAppointmentReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка записей по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsBookingAppointmentsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.BookingAppointment();
            var target2 = TestDataGenerator.BookingAppointment(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.BookingAppointment();
            var target4 = TestDataGenerator.BookingAppointment();
            await Context.BookingAppointments.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск записи в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.BookingAppointment();
            await Context.BookingAppointments.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск записи в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await bookingAppointmentReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленной записи в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.BookingAppointment(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.BookingAppointments.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingAppointmentReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
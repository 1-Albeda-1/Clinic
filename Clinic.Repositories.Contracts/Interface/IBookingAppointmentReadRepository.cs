using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="BookingAppointment"/>
    /// </summary>
    public interface IBookingAppointmentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="BookingAppointment"/>
        /// </summary>
        Task<List<BookingAppointment>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="BookingAppointment"/> по идентификатору
        /// </summary>
        Task<BookingAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Получить список <see cref="BookingAppointment"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, BookingAppointment>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
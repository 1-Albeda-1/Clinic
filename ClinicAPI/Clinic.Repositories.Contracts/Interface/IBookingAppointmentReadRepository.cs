using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts
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
    }
}
using Clinic.Context.Contracts.Models;
using System.Net.Sockets;

namespace Clinic.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="BookingAppointment"/>
    /// </summary>
    public interface IBookingAppointmentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="BookingAppointment"/>
        /// </summary>
        Task<IReadOnlyCollection<BookingAppointment>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="BookingAppointment"/> по идентификатору
        /// </summary>
        Task<BookingAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
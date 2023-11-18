using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Contracts.Interface
{
    public interface IBookingAppointmentService
    {
        /// <summary>
        /// Получить список всех <see cref="BookingAppointmentModel"/>
        /// </summary>
        Task<IEnumerable<BookingAppointmentModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="BookingAppointmentModel"/> по идентификатору
        /// </summary>
        Task<BookingAppointmentModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

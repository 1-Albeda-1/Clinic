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

        /// <summary>
        /// Добавляет новый кинотетар
        /// </summary>
        Task<BookingAppointmentModel> AddAsync(Guid patient, Guid timeTable, string? complaint, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий кинотеатр
        /// </summary>
        Task<BookingAppointmentModel> EditAsync(BookingAppointmentModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий кинотетар
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

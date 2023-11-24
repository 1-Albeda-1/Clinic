
using Clinic.Context.Contracts.Models;

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель записи на приём
    /// </summary>
    public class BookingAppointmentModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID клиента
        /// </summary>
        public PatientModel? Patient { get; set; }

        /// <summary>
        /// ID время приема пациента 
        /// </summary>
        public TimeTableModel? TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string? Сomplaint { get; set; } = string.Empty;
    }
}

using Clinic.Services.Contracts.Models;

namespace Clinic.API.Models
{
    /// <summary>
    /// Запись на приём
    /// </summary>
    public class BookingAppointmentResponse
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
        public string? Complaint { get; set; } = string.Empty;
    }
}

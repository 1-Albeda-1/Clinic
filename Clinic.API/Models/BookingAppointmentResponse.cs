using Clinic.Context.Contracts.Models;

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
        public PatientResponse? Patient { get; set; }

        /// <summary>
        /// ID время приема пациента 
        /// </summary>
        public TimeTableResponse? TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Сomplaint { get; set; } = string.Empty;
    }
}

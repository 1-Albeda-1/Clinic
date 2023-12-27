namespace Clinic.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания записи
    /// </summary>
    public class CreateBookingAppointmentRequest
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// ID время приема пациента 
        /// </summary>
        public Guid TimeTableId { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Complaint { get; set; } = string.Empty;
    }
}

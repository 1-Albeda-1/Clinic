namespace Clinic.API.Models.CreateRequest
{
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
        public string Сomplaint { get; set; } = string.Empty;
    }
}

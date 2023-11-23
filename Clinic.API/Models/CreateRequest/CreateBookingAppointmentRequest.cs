namespace Clinic.API.Models.CreateRequest
{
    public class CreateBookingAppointmentRequest
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        public Guid Patient { get; set; }

        /// <summary>
        /// ID время приема пациента 
        /// </summary>
        public Guid TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Сomplaint { get; set; } = string.Empty;
    }
}

namespace Clinic.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель запроса создания записи
    /// </summary>
    public class BookingAppointmentRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public string? Сomplaint { get; set; }
    }
}

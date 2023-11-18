using Clinic.Context.Contracts.Enums;

namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Запись на приём
    /// </summary>
    public class BookingAppointment : BaseAuditEntity
    {
        /// <summary>
        /// Номер клиента
        /// </summary>
        public Guid Patient_id { get; set; }

        /// <summary>
        /// Время приема пациента 
        /// </summary>
        public TimeTable? TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Сomplaint { get; set; } = string.Empty;
    }
}

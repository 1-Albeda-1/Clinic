
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
        public Guid? Patient { get; set; }

        /// <summary>
        /// ID время приема пациента 
        /// </summary>
        public Guid? TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Сomplaint { get; set; } = string.Empty;
    }
}

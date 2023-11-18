
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
        /// Номер клиента
        /// </summary>
        public Guid Patient_id { get; set; }

        /// <summary>
        /// Время приема пациента 
        /// </summary>
        public TimeTableModel? TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Сomplaint { get; set; } = string.Empty;
    }
}

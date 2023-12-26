namespace Clinic.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания расписания
    /// </summary>
    public class CreateTimeTableRequest
    {
        /// <summary>
        /// Время приема
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public int Office { get; set; }

        /// <summary>
        /// ID Врача
        /// </summary>
        public Guid DoctorId { get; set; }
    }
}

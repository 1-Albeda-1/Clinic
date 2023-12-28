namespace Clinic.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель запроса создания расписания
    /// </summary>
    public class TimeTableRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Время приема
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public int Office { get; set; }

        /// <summary>
        /// ID врача
        /// </summary>
        public Guid DoctorId { get; set; }
    }
}

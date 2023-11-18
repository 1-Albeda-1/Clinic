namespace Clinic.API.Models
{
    /// <summary>
    /// Рассписание 
    /// </summary>
    public class TimeTableResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Время приема
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public int Office { get; set; }

        /// <summary>
        /// ID Врача
        /// </summary>
        public Guid? Doctor { get; set; }
    }
}

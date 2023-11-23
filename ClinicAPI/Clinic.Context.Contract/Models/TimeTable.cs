using Clinic.Context.Contracts.Enums;


namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Рассписание 
    /// </summary>
    public class TimeTable : BaseAuditEntity
    {
        /// <summary>
        /// Время приема
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public int Office { get; set; }

        /// <summary>
        /// Врач
        /// </summary>
        public Doctor? Doctor { get; set; }
    }
}

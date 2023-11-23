using Clinic.Context.Contracts.Models;

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
        public DoctorResponse? Doctor { get; set; }
    }
}

using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель рассписания
    /// </summary>
    public class TimeTableModel
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
        public DoctorModel? Doctor { get; set; }
    }
}

using Clinic.Context.Contracts.Enums;


namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Пациент
    /// </summary>
    public class Patient : BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Полис
        /// </summary>
        public int Policy { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public string Birthday { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public MedClinic? MedClinic { get; set; }
    }
}

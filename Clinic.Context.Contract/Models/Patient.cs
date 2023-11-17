﻿


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
        public long Policy { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// ID поликлиники
        /// </summary>
        public Guid? MedClinic { get; set; }

        /// <summary>
        /// ID диагноза
        /// </summary>
        public Guid? Diagnosis { get; set; }
    }
}

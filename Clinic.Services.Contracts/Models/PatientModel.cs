using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель пациента
    /// </summary>
    public class PatientModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public DateTimeOffset Birthday { get; set; }

        /// <summary>
        /// ID поликлиники
        /// </summary>
        public MedClinicModel? MedClinic { get; set; }

        /// <summary>
        /// ID диагноза
        /// </summary>
        public DiagnosisModel? Diagnosis { get; set; }
    }
}

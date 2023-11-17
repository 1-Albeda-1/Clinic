using Clinic.Context.Contracts.Enums;

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель диагноза
    /// </summary>
    public class DiagnosisModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Диагноз
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Лекарство
        /// </summary>
        public string Medicament { get; set; } = string.Empty;
    }
}

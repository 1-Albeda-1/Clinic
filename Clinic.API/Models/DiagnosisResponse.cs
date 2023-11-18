namespace Clinic.API.Models
{
    //// <summary>
    /// Диагноз
    /// </summary>
    public class DiagnosisResponse
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

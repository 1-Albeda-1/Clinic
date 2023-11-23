namespace Clinic.Context.Contracts.Models
{
    //// <summary>
    /// Диагноз
    /// </summary>
    public class Diagnosis : BaseAuditEntity
    {
        /// <summary>
        /// Диагноз
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Лекарство
        /// </summary>
        public string Medicament { get; set; } = string.Empty;

        public ICollection<Patient> Patients { get; set; }
    }
}

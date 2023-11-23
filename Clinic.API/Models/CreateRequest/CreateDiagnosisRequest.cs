namespace Clinic.API.Models.CreateRequest
{
    public class CreateDiagnosisRequest
    {
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

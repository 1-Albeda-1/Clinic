using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания диагноза
    /// </summary>
    public class DiagnosisRequest : CreateDiagnosisRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

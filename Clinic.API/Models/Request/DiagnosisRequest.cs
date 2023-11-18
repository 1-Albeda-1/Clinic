using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    public class DiagnosisRequest : CreateDiagnosisRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

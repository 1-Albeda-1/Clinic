using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    public class PatientRequest : CreatePatientRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

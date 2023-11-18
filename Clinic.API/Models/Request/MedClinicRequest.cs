using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    public class MedClinicRequest : CreateMedClinicRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

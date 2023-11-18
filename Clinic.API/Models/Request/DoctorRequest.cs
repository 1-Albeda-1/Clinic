using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    public class DoctorRequest : CreateDoctorRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

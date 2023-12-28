using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания врача
    /// </summary>
    public class DoctorRequest : CreateDoctorRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

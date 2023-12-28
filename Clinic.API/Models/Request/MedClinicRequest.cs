using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания поликлиники
    /// </summary>
    public class MedClinicRequest : CreateMedClinicRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

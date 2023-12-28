using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания записи
    /// </summary>
    public class BookingAppointmentRequest : CreateBookingAppointmentRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    public class BookingAppointmentRequest : CreateBookingAppointmentRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

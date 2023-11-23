using Clinic.Services.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Contracts.Interface;


namespace Clinic.Services.Implementations
{
    public class BookingAppointmentService : IBookingAppointmentService
    {
        private readonly IBookingAppointmentService bookingAppointmentReadRepository;
        public BookingAppointmentService(IBookingAppointmentService bookingAppointmentReadRepository)
        {
            this.bookingAppointmentReadRepository = bookingAppointmentReadRepository;
        }
        async Task<IEnumerable<BookingAppointmentModel>> IBookingAppointmentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await bookingAppointmentReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new BookingAppointmentModel
            {
                Id = x.Id,
                Patient_id = x.Patient_id,
                TimeTable = (x.TimeTable != null) ? new TimeTableModel
                {
                    Id = x.TimeTable.Id,
                    Time = x.TimeTable.Time,
                    Office = x.TimeTable.Office,
                    Doctor = x.TimeTable.Doctor,                   
                } : null,
                Сomplaint = x.Сomplaint,
            });
        }

        async Task<BookingAppointmentModel?> IBookingAppointmentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await bookingAppointmentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new BookingAppointmentModel
            {
                Id = item.Id,
                Patient_id = item.Patient_id,
                TimeTable = (item.TimeTable != null) ? new TimeTableModel
                {
                    Id = item.TimeTable.Id,
                    Time = item.TimeTable.Time,
                    Office = item.TimeTable.Office,
                    Doctor = item.TimeTable.Doctor,
                } : null,
                Сomplaint = item.Сomplaint,
            };
        }
    }
}

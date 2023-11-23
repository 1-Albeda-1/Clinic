using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Repositories.Contracts;

namespace Clinic.Repositories
{
    public class BookingAppointmentReadRepository : IBookingAppointmentReadRepository
    {
        private readonly IClinicContext context;

        public BookingAppointmentReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<BookingAppointment>> IBookingAppointmentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.BookingAppointment.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Patient_id)
                .ToList());

        Task<BookingAppointment?> IBookingAppointmentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.BookingAppointment.FirstOrDefault(x => x.Id == id));
    }
}
using Clinic.Context.Contracts.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class BookingAppointmentReadRepository : IBookingAppointmentReadRepository, IReadRepositoryAnchor
    {
        private readonly IClinicContext context;

        public BookingAppointmentReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<BookingAppointment>> IBookingAppointmentReadRepository.GetAllAsync(CancellationToken cancellationToken)
           => Task.FromResult(context.BookingAppointment.Where(x => x.DeletedAt == null)
               .OrderBy(x => x.Patient)
               .ToList());

        Task<BookingAppointment?> IBookingAppointmentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.BookingAppointment.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, BookingAppointment>> IBookingAppointmentReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.BookingAppointment.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Patient)
                .ToDictionary(key => key.Id));
    }
}
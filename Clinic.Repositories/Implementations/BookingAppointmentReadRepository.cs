using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.Implementations
{
    public class BookingAppointmentReadRepository : IBookingAppointmentReadRepository, IReadRepositoryAnchor
    {
        private readonly IRead reader;

        public BookingAppointmentReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<BookingAppointment>> IBookingAppointmentReadRepository.GetAllAsync(CancellationToken cancellationToken)
           => reader.Read<BookingAppointment>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<BookingAppointment?> IBookingAppointmentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<BookingAppointment>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        //Task<Dictionary<Guid, BookingAppointment>> IBookingAppointmentReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        //    => reader.Read<BookingAppointment>()
        //        .ByIds(ids)
        //        .ToDictionaryAsync(x => x.Id, cancellationToken);
    }
}
using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;

namespace Clinic.Repositories.ReadRepositories
{
    public class BookingAppointmentReadRepository : IBookingAppointmentReadRepository, IRepositoryAnchor
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

        Task<Dictionary<Guid, BookingAppointment>> IBookingAppointmentReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
           => reader.Read<BookingAppointment>()
               .NotDeletedAt()
               .ByIds(ids)
               .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IBookingAppointmentReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<BookingAppointment>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
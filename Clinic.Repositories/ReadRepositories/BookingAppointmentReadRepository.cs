using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;
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
    }
}
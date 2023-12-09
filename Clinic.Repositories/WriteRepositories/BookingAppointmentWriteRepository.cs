using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using System.Net.Sockets;

namespace Clinic.Repositories.WriteRepositories
{
    public class BookingAppointmentWriteRepository : BaseWriteRepository<BookingAppointment>, IBookingAppointmentWriteRepository, IRepositoryAnchor
    {
        public BookingAppointmentWriteRepository(IWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}

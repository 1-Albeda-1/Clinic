using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;


namespace Clinic.Services.Implementations
{
    public class BookingAppointmentService : IBookingAppointmentService, IServiceAnchor
    {
        private readonly IBookingAppointmentReadRepository bookingAppointmentReadRepository;
        private readonly IMapper mapper;
        public BookingAppointmentService(IBookingAppointmentReadRepository bookingAppointmentReadRepository, IMapper mapper)
        {
            this.bookingAppointmentReadRepository = bookingAppointmentReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<BookingAppointmentModel>> IBookingAppointmentService.GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var result = await bookingAppointmentReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<BookingAppointmentModel>>(result);
        }

        async Task<BookingAppointmentModel?> IBookingAppointmentService.GetByIdAsync(System.Guid id, System.Threading.CancellationToken cancellationToken)
        {
            var item = await bookingAppointmentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            return mapper.Map<BookingAppointmentModel>(item);
        }
    }
}

using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Context.Contracts.Models;
using System.IO;
using System.Net.Sockets;


namespace Clinic.Services.Implementations
{
    public class BookingAppointmentService : IBookingAppointmentService, IServiceAnchor
    {
        private readonly IBookingAppointmentReadRepository bookingAppointmentReadRepository;
        private readonly IPatientReadRepository patientReadRepository;
        private readonly ITimeTableReadRepository timeTableReadRepository;
        private readonly IMapper mapper;
        public BookingAppointmentService(IBookingAppointmentReadRepository bookingAppointmentReadRepository, IMapper mapper, IPatientReadRepository patientReadRepository, ITimeTableReadRepository timeTableReadRepository)
        {
            this.bookingAppointmentReadRepository = bookingAppointmentReadRepository;
            this.mapper = mapper;
            this.patientReadRepository = patientReadRepository;
            this.timeTableReadRepository = timeTableReadRepository;
        }

        async Task<IEnumerable<BookingAppointmentModel>> IBookingAppointmentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var bookingAppointments = await bookingAppointmentReadRepository.GetAllAsync(cancellationToken);

            var patients = await patientReadRepository
                .GetByIdsAsync(bookingAppointments.Select(x => x.PatientId).Distinct(), cancellationToken);

            var timeTables = await timeTableReadRepository
                 .GetByIdsAsync(bookingAppointments.Select(x => x.TimeTableId).Distinct(), cancellationToken);

            var result = new List<BookingAppointmentModel>();

            foreach (var bookingAppointment in bookingAppointments)
            {
                if (!patients.TryGetValue(bookingAppointment.PatientId, out var patient) ||
                !timeTables.TryGetValue(bookingAppointment.TimeTableId, out var client))
                {
                    continue;
                }
                else
                {
                    var bookingAppointmentModel = mapper.Map<BookingAppointmentModel>(bookingAppointment);

                    bookingAppointmentModel.Patient = mapper.Map<PatientModel>(patient);
                    bookingAppointmentModel.TimeTable = mapper.Map<TimeTableModel>(timeTables);

                    result.Add(bookingAppointmentModel);
                }
            }
            return result;
        }

        async Task<BookingAppointmentModel?> IBookingAppointmentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await bookingAppointmentReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }

            var timeTable = await timeTableReadRepository.GetByIdAsync(item.TimeTableId, cancellationToken);
            var patient = await patientReadRepository.GetByIdAsync(item.PatientId, cancellationToken);
            var bookingAppointmentModel = mapper.Map<BookingAppointmentModel>(item);

            bookingAppointmentModel.Patient = mapper.Map<PatientModel>(patient);
            bookingAppointmentModel.TimeTable = mapper.Map<TimeTableModel>(timeTable);

            return bookingAppointmentModel;
        }
    }
}

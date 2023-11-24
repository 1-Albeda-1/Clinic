using AutoMapper;
using Clinic.Common.Interface;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Exceptions;
using System.IO;
using System.Net.Sockets;

namespace Clinic.Services.Implementations
{
    public class BookingAppointmentService : IBookingAppointmentService, IServiceAnchor
    {
        private readonly IBookingAppointmentReadRepository bookingAppointmentReadRepository;
        private readonly IBookingAppointmentWriteRepository bookingAppointmentWriteRepository;
        private readonly IPatientReadRepository patientReadRepository;
        private readonly ITimeTableReadRepository timeTableReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public BookingAppointmentService(IBookingAppointmentReadRepository bookingAppointmentReadRepository, IMapper mapper, 
            IPatientReadRepository patientReadRepository, ITimeTableReadRepository timeTableReadRepository, 
            IBookingAppointmentWriteRepository bookingAppointmentWriteRepository, IUnitOfWork unitOfWork)
        {
            this.bookingAppointmentReadRepository = bookingAppointmentReadRepository;
            this.mapper = mapper;
            this.patientReadRepository = patientReadRepository;
            this.timeTableReadRepository = timeTableReadRepository;
            this.bookingAppointmentWriteRepository = bookingAppointmentWriteRepository;
            this.unitOfWork = unitOfWork;
        }
        async Task<BookingAppointmentModel> IBookingAppointmentService.AddAsync(Guid patient, Guid timeTable, string? complaint, CancellationToken cancellationToken)
        {
            var item = new BookingAppointment
            {
                PatientId = patient,
                TimeTableId = timeTable,
                Сomplaint = complaint
            };

            bookingAppointmentWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var bookingAppointmentModel = mapper.Map<BookingAppointmentModel>(item);

            var patients = await patientReadRepository.GetByIdAsync(item.PatientId, cancellationToken);
            var timeTables = await timeTableReadRepository.GetByIdAsync(item.TimeTableId, cancellationToken);


            bookingAppointmentModel.Patient = mapper.Map<PatientModel>(patients);
            bookingAppointmentModel.TimeTable = mapper.Map<TimeTableModel>(timeTables);


            return bookingAppointmentModel;
        }

        async Task IBookingAppointmentService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetBookingAppointment = await bookingAppointmentReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetBookingAppointment == null)
            {
                throw new TimeTableEntityNotFoundException<BookingAppointment>(id);
            }

            if (targetBookingAppointment.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Запись на прием с идентификатором {id} уже удалена");
            }

            bookingAppointmentWriteRepository.Delete(targetBookingAppointment);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<BookingAppointmentModel> IBookingAppointmentService.EditAsync(BookingAppointmentModel source, CancellationToken cancellationToken)
        {
            var targetBookingAppointment = await bookingAppointmentReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetBookingAppointment == null)
            {
                throw new TimeTableEntityNotFoundException<BookingAppointment>(source.Id);
            }

            targetBookingAppointment.PatientId = source.Patient!.Id;
            targetBookingAppointment.TimeTableId = source.TimeTable!.Id;
            targetBookingAppointment.Сomplaint = source.Сomplaint;

            bookingAppointmentWriteRepository.Update(targetBookingAppointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            var bookingAppointmentModel = mapper.Map<BookingAppointmentModel>(targetBookingAppointment);

            var patient = await patientReadRepository.GetByIdAsync(targetBookingAppointment.PatientId, cancellationToken);
            var timeTable = await timeTableReadRepository.GetByIdAsync(targetBookingAppointment.TimeTableId, cancellationToken);

            bookingAppointmentModel.Patient = mapper.Map<PatientModel>(patient);
            bookingAppointmentModel.TimeTable = mapper.Map<TimeTableModel>(timeTable);

            return bookingAppointmentModel;
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

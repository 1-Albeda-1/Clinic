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
using Clinic.Services.Contracts.ModelsRequest;

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
        async Task<BookingAppointmentModel> IBookingAppointmentService.AddAsync(BookingAppointmentRequestModel model, CancellationToken cancellationToken)
        {
            var bookingAppointment = mapper.Map<BookingAppointment>(model);
            bookingAppointment.Сomplaint = model.Сomplaint;
            bookingAppointment.Patient = await patientReadRepository.GetByIdAsync(bookingAppointment.PatientId, cancellationToken);
            bookingAppointment.TimeTable = await timeTableReadRepository.GetByIdAsync(bookingAppointment.TimeTableId, cancellationToken);

            bookingAppointmentWriteRepository.Add(bookingAppointment);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var bookingAppointmentModel = mapper.Map<BookingAppointmentModel>(bookingAppointment);
            bookingAppointmentModel.Patient = mapper.Map<PatientModel>(bookingAppointment.Patient);
            bookingAppointmentModel.TimeTable = mapper.Map<TimeTableModel>(bookingAppointment.TimeTable);

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

        async Task<BookingAppointmentModel> IBookingAppointmentService.EditAsync(BookingAppointmentRequestModel model, CancellationToken cancellationToken)
        {
            var bookingAppointment = await bookingAppointmentReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (bookingAppointment == null)
            {
                throw new TimeTableEntityNotFoundException<BookingAppointment>(model.Id);
            }

            bookingAppointment.Patient = await patientReadRepository.GetByIdAsync(bookingAppointment.PatientId, cancellationToken);
            bookingAppointment.TimeTable = await timeTableReadRepository.GetByIdAsync(bookingAppointment.TimeTableId, cancellationToken);
            bookingAppointment.Сomplaint = model.Сomplaint;

            bookingAppointmentWriteRepository.Update(bookingAppointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            var bookingAppointmentModel = mapper.Map<BookingAppointmentModel>(bookingAppointment);

            var patient = await patientReadRepository.GetByIdAsync(bookingAppointment.PatientId, cancellationToken);
            var timeTable = await timeTableReadRepository.GetByIdAsync(bookingAppointment.TimeTableId, cancellationToken);

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

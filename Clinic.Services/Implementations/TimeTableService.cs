using AutoMapper;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Exceptions;
using System.IO;
using System.Net.Sockets;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.Services.Implementations
{
    /// <inheritdoc cref="ITimeTableService"/>
    public class TimeTableService : ITimeTableService, IServiceAnchor
    {
        private readonly ITimeTableReadRepository timeTableReadRepository;
        private readonly ITimeTableWriteRepository timeTableWriteRepository;
        private readonly IDoctorReadRepository doctorReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TimeTableService(ITimeTableReadRepository timeTableReadRepository, 
        ITimeTableWriteRepository timeTableWriteRepository, 
        IUnitOfWork unitOfWork, 
        IDoctorReadRepository doctorReadRepository,
        IMapper mapper)
        {
            this.timeTableReadRepository = timeTableReadRepository;
            this.mapper = mapper;
            this.doctorReadRepository = doctorReadRepository;
            this.timeTableWriteRepository = timeTableWriteRepository;
            this.unitOfWork = unitOfWork;
        }
        async Task<TimeTableModel> ITimeTableService.AddAsync(TimeTableRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var timeTable = mapper.Map<TimeTable>(model);
            timeTableWriteRepository.Add(timeTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTimeTableModelOnMapping(timeTable, cancellationToken);
        }

        async Task ITimeTableService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTimeTable = await timeTableReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetTimeTable == null)
            {
                throw new ClinicEntityNotFoundException<TimeTable>(id);
            }

            timeTableWriteRepository.Delete(targetTimeTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<TimeTableModel> ITimeTableService.EditAsync(TimeTableRequestModel model, CancellationToken cancellationToken)
        {
            var timeTable = await timeTableReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (timeTable == null)
            {
                throw new ClinicEntityNotFoundException<TimeTable>(model.Id);
            }

            timeTable = mapper.Map<TimeTable>(model);
            timeTableWriteRepository.Update(timeTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTimeTableModelOnMapping(timeTable, cancellationToken);
        }
        async Task<IEnumerable<TimeTableModel>> ITimeTableService.GetAllAsync(CancellationToken cancellationToken)
        {
            var timeTables = await timeTableReadRepository.GetAllAsync(cancellationToken);
            var doctors = await doctorReadRepository
                .GetByIdsAsync(timeTables.Select(x => x.DoctorId).Distinct(), cancellationToken);

            var result = new List<TimeTableModel>();

            foreach (var timeTable in timeTables)
            {
                if (!doctors.TryGetValue(timeTable.DoctorId, out var doctor))
                {
                    continue;
                }
                else
                {
                    var timeTableModel = mapper.Map<TimeTableModel>(timeTable);

                    timeTableModel.Doctor = mapper.Map<DoctorModel>(doctor);

                    result.Add(timeTableModel);
                }
            }
            return result;
        }

        async Task<TimeTableModel?> ITimeTableService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new ClinicEntityNotFoundException<TimeTable>(id);
            }

            return await GetTimeTableModelOnMapping(item, cancellationToken);
        }

        async private Task<TimeTableModel> GetTimeTableModelOnMapping(TimeTable timeTable, CancellationToken cancellationToken)
        {
            var ticketModel = mapper.Map<TimeTableModel>(timeTable);
            ticketModel.Doctor = mapper.Map<DoctorModel>(await doctorReadRepository.GetByIdAsync(timeTable.DoctorId, cancellationToken));

            return ticketModel;
        }
    }
}

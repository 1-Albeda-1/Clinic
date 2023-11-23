using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using System.IO;

namespace Clinic.Services.Implementations
{
    public class TimeTableService : ITimeTableService, IServiceAnchor
    {
        private readonly ITimeTableReadRepository timeTableReadRepository;
        private readonly IDoctorReadRepository doctorReadRepository;
        private readonly IMapper mapper;
        public TimeTableService(ITimeTableReadRepository timeTableReadRepository, IMapper mapper, IDoctorReadRepository doctorReadRepository)
        {
            this.timeTableReadRepository = timeTableReadRepository;
            this.mapper = mapper;
            this.doctorReadRepository = doctorReadRepository;
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
                return null;
            }

            var doctor = await doctorReadRepository.GetByIdAsync(item.DoctorId, cancellationToken);
            var timeTableModel = mapper.Map<TimeTableModel>(item);

            timeTableModel.Doctor = mapper.Map<DoctorModel>(doctor);

            return timeTableModel;
        }
    }
}

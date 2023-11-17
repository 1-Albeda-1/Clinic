using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Implementations
{
    public class TimeTableService : ITimeTableService, IServiceAnchor
    {
        private readonly ITimeTableReadRepository timeTableReadRepository;
        private readonly IMapper mapper;
        public TimeTableService(ITimeTableReadRepository timeTableReadRepository, IMapper mapper)
        {
            this.timeTableReadRepository = timeTableReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<TimeTableModel>> ITimeTableService.GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var result = await timeTableReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<TimeTableModel>>(result);
        }

        async Task<TimeTableModel?> ITimeTableService.GetByIdAsync(System.Guid id, System.Threading.CancellationToken cancellationToken)
        {
            var item = await timeTableReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            return mapper.Map<TimeTableModel>(item);
        }
    }
}

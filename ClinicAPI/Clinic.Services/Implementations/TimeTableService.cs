using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Services.Implementations
{
    public class TimeTableService : ITimeTableService
    {
        private readonly ITimeTableReadRepository timeTableReadRepository;
        public TimeTableService(ITimeTableReadRepository timeTableReadRepository)
        {
            this.timeTableReadRepository = timeTableReadRepository;
        }
        async Task<IEnumerable<TimeTableModel>> ITimeTableService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await timeTableReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new TimeTableModel
            {
                Id = x.Id,
                Time = x.Time,
                Doctor = x.Doctor,
            });
        }

        async Task<TimeTableModel?> ITimeTableService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new TimeTableModel
            {
                Id = item.Id,
                Time = item.Time,
                Doctor = item.Doctor,
            };
        }
    }
}

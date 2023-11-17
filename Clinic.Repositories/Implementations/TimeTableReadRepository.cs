using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class TimeTableReadRepository : ITimeTableReadRepository, IReadRepositoryAnchor
    {
        private readonly IClinicContext context;

        public TimeTableReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<TimeTable>> ITimeTableReadRepository.GetAllAsync(CancellationToken cancellationToken)
             => Task.FromResult(context.TimeTable.Where(x => x.DeletedAt == null)
                 .OrderBy(x => x.Time)
                 .ToList());

        Task<TimeTable?> ITimeTableReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.TimeTable.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, TimeTable>> ITimeTableReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.TimeTable.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Time)
                .ToDictionary(key => key.Id));
    }
}

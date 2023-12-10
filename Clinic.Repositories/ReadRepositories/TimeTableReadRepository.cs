using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Clinic.Repositories.ReadRepositories
{
    public class TimeTableReadRepository : ITimeTableReadRepository, IRepositoryAnchor
    {
        private readonly IRead reader;

        public TimeTableReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<TimeTable>> ITimeTableReadRepository.GetAllAsync(CancellationToken cancellationToken)
             => reader.Read<TimeTable>()
                .OrderBy(x => x.Time)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<TimeTable?> ITimeTableReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<TimeTable>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, TimeTable>> ITimeTableReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<TimeTable>()
                .ByIds(ids)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> ITimeTableReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<TimeTable>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}

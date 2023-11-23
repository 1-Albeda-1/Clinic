using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.Implementations
{
    public class DoctorReadRepository : IDoctorReadRepository, IReadRepositoryAnchor
    {
        private readonly IRead reader;

        public DoctorReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Doctor>> IDoctorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Doctor>()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Surname)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Doctor?> IDoctorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Doctor>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Doctor>> IDoctorReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Doctor>()
                .ByIds(ids)
                .ToDictionaryAsync(x => x.Id, cancellationToken);
    }
}

using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.ReadRepositories
{
    public class DoctorReadRepository : IDoctorReadRepository, IRepositoryAnchor
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

        Task<bool> IDoctorReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Doctor>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}

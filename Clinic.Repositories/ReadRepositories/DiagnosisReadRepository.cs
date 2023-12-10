using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.ReadRepositories
{
    public class DiagnosisReadRepository : IDiagnosisReadRepository, IRepositoryAnchor
    {
        private readonly IRead reader;

        public DiagnosisReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Diagnosis>> IDiagnosisReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Diagnosis>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Diagnosis?> IDiagnosisReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Diagnosis>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Diagnosis>> IDiagnosisReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Diagnosis>()
                .ByIds(ids)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IDiagnosisReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Diagnosis>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}

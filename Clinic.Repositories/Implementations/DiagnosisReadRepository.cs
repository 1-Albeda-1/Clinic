using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.Implementations
{
    public class DiagnosisReadRepository : IDiagnosisReadRepository, IReadRepositoryAnchor
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
    }
}

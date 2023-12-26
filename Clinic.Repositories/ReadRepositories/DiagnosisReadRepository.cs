using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IDiagnosisReadRepository"/>
    /// </summary>
    public class DiagnosisReadRepository : IDiagnosisReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
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
                .NotDeletedAt()
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Diagnosis>> IDiagnosisReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Diagnosis>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IDiagnosisReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Diagnosis>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}

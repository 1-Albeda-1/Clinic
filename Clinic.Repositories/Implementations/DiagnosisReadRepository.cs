using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class DiagnosisReadRepository : IDiagnosisReadRepository, IReadRepositoryAnchor
    {
        private readonly IClinicContext context;

        public DiagnosisReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<Diagnosis>> IDiagnosisReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Diagnosis.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Diagnosis?> IDiagnosisReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Diagnosis.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Diagnosis>> IDiagnosisReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Diagnosis.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(key => key.Id));
    }
}

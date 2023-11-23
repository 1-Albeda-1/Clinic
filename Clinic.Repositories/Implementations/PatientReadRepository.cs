using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.Implementations
{
    public class PatientReadRepository : IPatientReadRepository,  IReadRepositoryAnchor
    {
        private readonly IRead reader;

        public PatientReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Patient>> IPatientReadRepository.GetAllAsync(CancellationToken cancellationToken)
             => reader.Read<Patient>()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Surname)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Patient?> IPatientReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Patient>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Patient>> IPatientReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Patient>()
                .ByIds(ids)
                .ToDictionaryAsync(x => x.Id, cancellationToken);
    }
}

using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories.Implementations
{
    public class MedClinicReadRepository : IMedClinicReadRepository, IReadRepositoryAnchor
    {
        private readonly IRead reader;

        public MedClinicReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<MedClinic>> IMedClinicReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<MedClinic>()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<MedClinic?> IMedClinicReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<MedClinic>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, MedClinic>> IMedClinicReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<MedClinic>()
                .ByIds(ids)
                .ToDictionaryAsync(x => x.Id, cancellationToken);
    }
}

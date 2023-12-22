using Clinic.Common.Interface;
using Clinic.Common.Repositories;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Clinic.Repositories.ReadRepositories
{
    public class MedClinicReadRepository : IMedClinicReadRepository, IRepositoryAnchor
    {
        private readonly IRead reader;

        public MedClinicReadRepository(IRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<MedClinic>> IMedClinicReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<MedClinic>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<MedClinic?> IMedClinicReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<MedClinic>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, MedClinic>> IMedClinicReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<MedClinic>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IMedClinicReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<MedClinic>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}

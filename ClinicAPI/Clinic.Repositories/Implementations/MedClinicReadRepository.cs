using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class MedClinicReadRepository : IMedClinicReadRepository
    {
        private readonly IClinicContext context;

        public MedClinicReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<MedClinic>> IMedClinicReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.MedClinic.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<MedClinic?> IMedClinicReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.MedClinic.FirstOrDefault(x => x.Id == id));
    }
}

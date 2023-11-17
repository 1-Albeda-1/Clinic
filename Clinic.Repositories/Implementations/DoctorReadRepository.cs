using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;
namespace Clinic.Repositories.Implementations
{
    public class DoctorReadRepository : IDoctorReadRepository, IReadRepositoryAnchor
    {
        private readonly IClinicContext context;

        public DoctorReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<Doctor>> IDoctorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Doctor.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Doctor?> IDoctorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Doctor.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Doctor>> IDoctorReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Doctor.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(key => key.Id));
    }
}

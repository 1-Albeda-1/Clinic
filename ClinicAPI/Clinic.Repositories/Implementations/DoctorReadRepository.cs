using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class DoctorReadRepository : IDoctorReadRepository
    {
        private readonly IClinicContext context;

        public DoctorReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<Doctor>> IDoctorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Doctor.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Department)
                .ToList());

        Task<Doctor?> IDoctorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Doctor.FirstOrDefault(x => x.Id == id));
    }
}

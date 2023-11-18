using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class PatientReadRepository : IPatientReadRepository
    {
        private readonly IClinicContext context;

        public PatientReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<Patient>> IPatientReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Patient.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Patient?> IPatientReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Patient.FirstOrDefault(x => x.Id == id));
    }
}

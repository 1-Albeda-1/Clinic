using Clinic.Context.Contracts.Models;
using Clinic.Context.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Repositories.Implementations
{
    public class DepartmentReadRepository : IDepartmentReadRepository
    {
        private readonly IClinicContext context;

        public DepartmentReadRepository(IClinicContext context)
        {
            this.context = context;
        }

        Task<List<Department>> IDepartmentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Department.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Department?> IDepartmentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Department.FirstOrDefault(x => x.Id == id));
    }
}

using Clinic.Services.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Contracts.Interface;

namespace Clinic.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentService departmentReadRepository;
        public DepartmentService(IDepartmentReadRepository departmentReadRepository)
        {
            this.departmentReadRepository = departmentReadRepository;
        }
        async Task<IEnumerable<DepartmentModel>> IDepartmentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await departmentReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new DepartmentModel
            {
                Id = x.Id,
                Name = x.Name,            
            });
        }

        async Task<DepartmentModel?> IDepartmentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await departmentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new DepartmentModel
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
    }

}

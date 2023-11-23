using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorReadRepository doctorReadRepository;
        public DoctorService(IDoctorReadRepository doctorReadRepository)
        {
            this.doctorReadRepository = doctorReadRepository;
        }
        async Task<IEnumerable<DoctorModel>> IDoctorService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await doctorReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new DoctorModel
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,    
                Patronymic = x.Patronymic,
                DocumentType = x.DocumentType,
                CategoriesTypes = x.CategoriesTypes,
                Department = x.Department,
            });
        }

        async Task<DoctorModel?> IDoctorService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await doctorReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new DoctorModel
            {
                Id = item.Id,
                Surname = item.Surname,
                Name = item.Name,                
                Patronymic = item.Patronymic,
                DocumentType = item.DocumentType,
                CategoriesTypes = item.CategoriesTypes,
                Department = item.Department,
            };
        }
    }
}

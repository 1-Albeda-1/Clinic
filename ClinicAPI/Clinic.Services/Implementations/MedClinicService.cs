using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.Interface;
using Clinic.Repositories.Contracts.Interface;

namespace Clinic.Services.Implementations
{
    public class MedClinicService : IMedClinicService
    {
        private readonly IMedClinicReadRepository medClinicReadRepository;
        public MedClinicService(IMedClinicReadRepository medClinicReadRepository)
        {
            this.medClinicReadRepository = medClinicReadRepository;
        }
        async Task<IEnumerable<MedClinicModel>> IMedClinicService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await medClinicReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new MedClinicModel
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name,
            });
        }

        async Task<MedClinicModel?> IMedClinicService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await medClinicReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new MedClinicModel
            {
                Id = item.Id,
                Address = item.Address,
                Name = item.Name,
            };
        }
    }

}

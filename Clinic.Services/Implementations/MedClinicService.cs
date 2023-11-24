using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Implementations
{
    public class MedClinicService : IMedClinicService, IServiceAnchor
    {
        private readonly IMedClinicReadRepository medClinicReadRepository;
        private readonly IMapper mapper;
        public MedClinicService(IMedClinicReadRepository medClinicReadRepository, IMapper mapper)
        {
            this.medClinicReadRepository = medClinicReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<MedClinicModel>> IMedClinicService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await medClinicReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<MedClinicModel>(x));
        }

        async Task<MedClinicModel?> IMedClinicService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await medClinicReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }
            return mapper.Map<MedClinicModel>(item);
        }
    }

}

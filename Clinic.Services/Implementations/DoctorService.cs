using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Implementations
{
    public class DoctorService : IDoctorService,  IServiceAnchor
    {
        private readonly IDoctorReadRepository doctorReadRepository;
        private readonly IMapper mapper;
        public DoctorService(IDoctorReadRepository doctorReadRepository, IMapper mapper)
        {
            this.doctorReadRepository = doctorReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<DoctorModel>> IDoctorService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await doctorReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<DoctorModel>(x));
        }

        async Task<DoctorModel?> IDoctorService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await doctorReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }
            return mapper.Map<DoctorModel>(item);
        }
    }
}

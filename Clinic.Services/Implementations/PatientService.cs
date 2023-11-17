using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Implementations
{
    public class PatientService : IPatientService, IServiceAnchor
    {
        private readonly IPatientReadRepository patientReadRepository;
        private readonly IMapper mapper;
        public PatientService(IPatientReadRepository patientReadRepository, IMapper mapper)
        {
            this.patientReadRepository = patientReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<PatientModel>> IPatientService.GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var result = await patientReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PatientModel>>(result);
        }

        async Task<PatientModel?> IPatientService.GetByIdAsync(System.Guid id, System.Threading.CancellationToken cancellationToken)
        {
            var item = await patientReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            return mapper.Map<PatientModel>(item);
        }
    }
}

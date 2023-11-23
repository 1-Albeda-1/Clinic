using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Implementations
{
    public class DiagnosisService : IDiagnosisService, IServiceAnchor
    {
        private readonly IDiagnosisReadRepository diagnosisReadRepository;
        private readonly IMapper mapper;
        public DiagnosisService(IDiagnosisReadRepository diagnosisReadRepository, IMapper mapper)
        {
            this.diagnosisReadRepository = diagnosisReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<DiagnosisModel>> IDiagnosisService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await diagnosisReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<DiagnosisModel>(x));
        }

        async Task<DiagnosisModel?> IDiagnosisService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await diagnosisReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }
            return mapper.Map<DiagnosisModel>(item);
        }
    }
}

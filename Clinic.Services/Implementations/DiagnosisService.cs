using AutoMapper;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Common.Interface;
using Clinic.Services.Contracts.Exceptions;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;

namespace Clinic.Services.Implementations
{
    public class DiagnosisService : IDiagnosisService, IServiceAnchor
    {
        private readonly IDiagnosisReadRepository diagnosisReadRepository;
        private readonly IDiagnosisWriteRepository diagnosisWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
       
        public DiagnosisService(IDiagnosisReadRepository diagnosisReadRepository, IDiagnosisWriteRepository diagnosisWriteRepository,
        IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.diagnosisReadRepository = diagnosisReadRepository;
            this.diagnosisWriteRepository = diagnosisWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        async Task<DiagnosisModel> IDiagnosisService.AddAsync(DiagnosisModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            var item = mapper.Map<Diagnosis>(model);
            diagnosisWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<DiagnosisModel>(item);
        }

        async Task IDiagnosisService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetDiagnosis = await diagnosisReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetDiagnosis == null)
            {
                throw new ClinicEntityNotFoundException<Diagnosis>(id);
            }

            diagnosisWriteRepository.Delete(targetDiagnosis);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<DiagnosisModel> IDiagnosisService.EditAsync(DiagnosisModel source, CancellationToken cancellationToken)
        {
            var targetDiagnosis = await diagnosisReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetDiagnosis == null)
            {
                throw new ClinicEntityNotFoundException<Diagnosis>(source.Id);
            }

            targetDiagnosis = mapper.Map<Diagnosis>(source);

            diagnosisWriteRepository.Update(targetDiagnosis);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DiagnosisModel>(targetDiagnosis);
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
                throw new ClinicEntityNotFoundException<Diagnosis>(id);
            }
            return mapper.Map<DiagnosisModel>(item);
        }
    }
}

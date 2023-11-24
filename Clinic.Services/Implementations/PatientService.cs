using AutoMapper;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using System.IO;

namespace Clinic.Services.Implementations
{
    public class PatientService : IPatientService, IServiceAnchor
    {
        private readonly IPatientReadRepository patientReadRepository;
        private readonly IDiagnosisReadRepository diagnosisReadRepository;
        private readonly IMedClinicReadRepository medClinicReadRepository;
        private readonly IMapper mapper;
        public PatientService(IPatientReadRepository patientReadRepository, IMapper mapper, IDiagnosisReadRepository diagnosisReadRepository, IMedClinicReadRepository medClinicReadRepository)
        {
            this.patientReadRepository = patientReadRepository;
            this.mapper = mapper;
            this.diagnosisReadRepository = diagnosisReadRepository;
            this.medClinicReadRepository = medClinicReadRepository;
        }
        async Task<IEnumerable<PatientModel>> IPatientService.GetAllAsync(CancellationToken cancellationToken)
        {
            var patients = await patientReadRepository.GetAllAsync(cancellationToken);

            var diagnosises = await diagnosisReadRepository
              .GetByIdsAsync(patients.Select(x => x.DiagnosisId).Distinct(), cancellationToken);

            var medClinics = await medClinicReadRepository
                .GetByIdsAsync(patients.Where(x => x.MedClinicId.HasValue).Select(x => x.MedClinicId!.Value).Distinct(), cancellationToken);
          

            var result = new List<PatientModel>();

            foreach (var patient in patients)
            {
                if (!diagnosises.TryGetValue(patient.DiagnosisId, out var diagnosis))
                {
                    continue;
                }
                else
                {
                    var patientModel = mapper.Map<PatientModel>(patient);

                    patientModel.Diagnosis = mapper.Map<DiagnosisModel>(diagnosis);
                    patientModel.MedClinic = patient.MedClinicId.HasValue &&
                                              medClinics.TryGetValue(patient.MedClinicId!.Value, out var staff)
                        ? mapper.Map<MedClinicModel>(staff)
                        : null;

                    result.Add(patientModel);
                }
            }
            return result;
        }

        async Task<PatientModel?> IPatientService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await patientReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }

            var diagnosis = await diagnosisReadRepository.GetByIdAsync(item.DiagnosisId, cancellationToken);
            var patientModel = mapper.Map<PatientModel>(item);

            patientModel.Diagnosis = mapper.Map<DiagnosisModel>(diagnosis);
            patientModel.MedClinic = item.MedClinicId.HasValue ?
                            mapper.Map<MedClinicModel>(await medClinicReadRepository.GetByIdAsync(item.MedClinicId.Value, cancellationToken))
                            : null;

            return patientModel;
        }
    }
}

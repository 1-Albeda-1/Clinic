using AutoMapper;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Models;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Exceptions;
using System.IO;
using System.Net.Sockets;

namespace Clinic.Services.Implementations
{
    public class PatientService : IPatientService, IServiceAnchor
    {
        private readonly IPatientReadRepository patientReadRepository;
        private readonly IPatientWriteRepository patientWriteRepository;
        private readonly IDiagnosisReadRepository diagnosisReadRepository;
        private readonly IMedClinicReadRepository medClinicReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public PatientService(IPatientReadRepository patientReadRepository, IMapper mapper, 
            IDiagnosisReadRepository diagnosisReadRepository, IMedClinicReadRepository medClinicReadRepository, 
            IPatientWriteRepository patientWriteRepository, IUnitOfWork unitOfWork)
        {
            this.patientReadRepository = patientReadRepository;
            this.mapper = mapper;
            this.diagnosisReadRepository = diagnosisReadRepository;
            this.medClinicReadRepository = medClinicReadRepository;
            this.patientWriteRepository = patientWriteRepository;
            this.unitOfWork = unitOfWork;
        }
        async Task<PatientModel> IPatientService.AddAsync(string surname, string name, string patronymic, string phone, long policy, 
            DateTimeOffset birthday, Guid? medClinic, Guid diagnosis, CancellationToken cancellationToken)
        {
            var item = new Patient
            {
                Surname = surname,
                Name = name,
                Patronymic = patronymic,
                Phone = phone,
                Policy = policy,
                Birthday = birthday,
                MedClinicId = medClinic,
                DiagnosisId = diagnosis
            };

            patientWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var patientModel = mapper.Map<PatientModel>(item);

            var diagnosises = await diagnosisReadRepository.GetByIdAsync(item.DiagnosisId, cancellationToken);

            patientModel.Diagnosis = mapper.Map<DiagnosisModel>(diagnosises);
            patientModel.MedClinic = item.MedClinicId.HasValue ?
                mapper.Map<MedClinicModel>(await medClinicReadRepository.GetByIdAsync(item.MedClinicId.Value, cancellationToken))
                : null;

            return patientModel;
        }

        async Task IPatientService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPatient = await patientReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetPatient == null)
            {
                throw new TimeTableEntityNotFoundException<Patient>(id);
            }

            if (targetPatient.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Пациент с идентификатором {id} уже удален");
            }

            patientWriteRepository.Delete(targetPatient);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PatientModel> IPatientService.EditAsync(PatientModel source, CancellationToken cancellationToken)
        {
            var targetPatient = await patientReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetPatient == null)
            {
                throw new TimeTableEntityNotFoundException<Patient>(source.Id);
            }

            targetPatient.Surname = source.Surname;
            targetPatient.Name = source.Name;
            targetPatient.Patronymic = source.Patronymic;
            targetPatient.Phone = source.Phone;
            targetPatient.Policy = source.Policy;
            targetPatient.Birthday = source.Birthday;
            targetPatient.MedClinicId = source.MedClinic != null ? source.MedClinic.Id : Guid.Empty;
            targetPatient.DiagnosisId = source.Diagnosis!.Id;

            patientWriteRepository.Update(targetPatient);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            var patientModel = mapper.Map<PatientModel>(targetPatient);

            var diagnosis = await diagnosisReadRepository.GetByIdAsync(targetPatient.DiagnosisId, cancellationToken);

            patientModel.Diagnosis = mapper.Map<DiagnosisModel>(diagnosis);
            patientModel.MedClinic = targetPatient.MedClinicId.HasValue ?
                mapper.Map<MedClinicModel>(await medClinicReadRepository.GetByIdAsync(targetPatient.MedClinicId.Value, cancellationToken))
                : null;

            return patientModel;
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

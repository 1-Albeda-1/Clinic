﻿using AutoMapper;
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
using Clinic.Services.Contracts.ModelsRequest;

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
        async Task<PatientModel> IPatientService.AddAsync(PatientRequestModel model, CancellationToken cancellationToken)
        {
            var patient = mapper.Map<Patient>(model);
            patient.Surname = model.Surname;
            patient.Name = model.Name;
            patient.Patronymic = model.Patronymic;
            patient.Phone = model.Phone;
            patient.Policy = model.Policy;
            patient.Birthday = model.Birthday;
            patient.Diagnosis = await diagnosisReadRepository.GetByIdAsync(patient.DiagnosisId, cancellationToken);
            patient.MedClinic = patient.MedClinicId.HasValue ?
                await medClinicReadRepository.GetByIdAsync(patient.MedClinicId.Value, cancellationToken)
                : null;

            patientWriteRepository.Add(patient);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var patientModel = mapper.Map<PatientModel>(patient);

            
            patientModel.Diagnosis = mapper.Map<DiagnosisModel>(patient.Diagnosis);
            patientModel.MedClinic = patient.MedClinicId.HasValue ?
                mapper.Map<MedClinicModel>(await medClinicReadRepository.GetByIdAsync(patient.MedClinicId.Value, cancellationToken))
                : null;

            return patientModel;
        }

        async Task IPatientService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPatient = await patientReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetPatient == null)
            {
                throw new ClinicEntityNotFoundException<Patient>(id);
            }

            if (targetPatient.DeletedAt.HasValue)
            {
                throw new ClinicInvalidOperationException($"Пациент с идентификатором {id} уже удален");
            }

            patientWriteRepository.Delete(targetPatient);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PatientModel> IPatientService.EditAsync(PatientRequestModel model, CancellationToken cancellationToken)
        {
            var patient = await patientReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (patient == null)
            {
                throw new ClinicEntityNotFoundException<Patient>(model.Id);
            }

            patient.Surname = model.Surname;
            patient.Name = model.Name;
            patient.Patronymic = model.Patronymic;
            patient.Phone = model.Phone;
            patient.Policy = model.Policy;
            patient.Birthday = model.Birthday;      
            patient.Diagnosis = await diagnosisReadRepository.GetByIdAsync(patient.DiagnosisId, cancellationToken);
            patient.MedClinicId = model.MedClinic;
            patient.MedClinic = model.MedClinic.HasValue ?
                await medClinicReadRepository.GetByIdAsync(model.MedClinic.Value, cancellationToken)
                : null;           

            patientWriteRepository.Update(patient);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            var patientModel = mapper.Map<PatientModel>(patient);

            patientModel.Diagnosis = mapper.Map<DiagnosisModel>(patient.Diagnosis);
            patientModel.MedClinic = mapper.Map<MedClinicModel>(patient.MedClinic);

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

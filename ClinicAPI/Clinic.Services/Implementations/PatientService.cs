using Clinic.Services.Contracts.Models;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Contracts;

namespace Clinic.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientReadRepository patientReadRepository;
        public PatientService(IPatientReadRepository patientReadRepository)
        {
            this.patientReadRepository = patientReadRepository;
        }
        async Task<IEnumerable<PatientModel>> IPatientService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await patientReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new PatientModel
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                Policy = x.Policy,
                Birthday = x.Birthday,
                MedClinic = x.MedClinic
            });
        }

        async Task<PatientModel?> IPatientService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await patientReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new PatientModel
            {
                Id = item.Id,
                Surname = item.Surname,
                Name = item.Name,
                Patronymic = item.Patronymic,
                Phone = item.Phone,
                Policy = item.Policy,
                Birthday = item.Birthday,
                MedClinic = item.MedClinic,
            };
        }
    }
}

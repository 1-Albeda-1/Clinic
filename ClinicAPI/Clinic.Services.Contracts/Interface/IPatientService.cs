using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Contracts
{
    public interface IPatientService
    {
        /// <summary>
        /// Получить список всех <see cref="PatientModel"/>
        /// </summary>
        Task<IEnumerable<PatientModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PatientModel"/> по идентификатору
        /// </summary>
        Task<PatientModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;

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

        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        Task<PatientModel> AddAsync(PatientRequestModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего клиента
        /// </summary>
        Task<PatientModel> EditAsync(PatientRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
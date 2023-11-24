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

        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        Task<PatientModel> AddAsync(string surname, string name, string patronymic, string phone, long policy, DateTimeOffset birthday, Guid? medClinic, Guid diagnosis, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего клиента
        /// </summary>
        Task<PatientModel> EditAsync(PatientModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
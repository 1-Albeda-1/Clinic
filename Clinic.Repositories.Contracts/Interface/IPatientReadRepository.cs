using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Patient"/>
    /// </summary>
    public interface IPatientReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Patient"/>
        /// </summary>
        Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Patient"/> по идентификатору
        /// </summary>
        Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Patient"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Patient>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}

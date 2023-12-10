using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Diagnosis"/>
    /// </summary>
    public interface IDiagnosisReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Diagnosis"/>
        /// </summary>
        Task<IReadOnlyCollection<Diagnosis>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Diagnosis"/> по идентификатору
        /// </summary>
        Task<Diagnosis?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Diagnosis"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Diagnosis>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Diagnosis"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}

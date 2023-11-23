using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="TimeTable"/>
    /// </summary>
    public interface ITimeTableReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="TimeTable"/>
        /// </summary>
        Task<IReadOnlyCollection<TimeTable>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TimeTable"/> по идентификатору
        /// </summary>
        Task<TimeTable?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TimeTable"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, TimeTable>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}

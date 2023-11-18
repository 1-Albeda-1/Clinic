using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Department"/>
    /// </summary>
    public interface IDepartmentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Department"/>
        /// </summary>
        Task<List<Department>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Department"/> по идентификатору
        /// </summary>
        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

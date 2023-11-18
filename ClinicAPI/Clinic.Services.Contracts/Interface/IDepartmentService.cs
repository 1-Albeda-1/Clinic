using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Contracts.Interface
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Получить список всех <see cref="DepartmentModel"/>
        /// </summary>
        Task<IEnumerable<DepartmentModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DepartmentModel"/> по идентификатору
        /// </summary>
        Task<DepartmentModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

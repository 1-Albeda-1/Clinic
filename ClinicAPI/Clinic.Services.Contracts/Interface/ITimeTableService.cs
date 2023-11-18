using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Contracts.Interface
{
    public interface ITimeTableService
    {
        /// <summary>
        /// Получить список всех <see cref="TimeTableModel"/>
        /// </summary>
        Task<IEnumerable<TimeTableModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TimeTableModel"/> по идентификатору
        /// </summary>
        Task<TimeTableModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

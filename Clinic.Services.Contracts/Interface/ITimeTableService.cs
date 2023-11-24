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

        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        Task<TimeTableModel> AddAsync(DateTimeOffset time, int office, Guid doctor, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего клиента
        /// </summary>
        Task<TimeTableModel> EditAsync(TimeTableModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

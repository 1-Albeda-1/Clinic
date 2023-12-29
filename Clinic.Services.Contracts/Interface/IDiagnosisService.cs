using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Contracts.Interface
{
    public interface IDiagnosisService
    {
        /// <summary>
        /// Получить список всех <see cref="DiagnosisModel"/>
        /// </summary>
        Task<IEnumerable<DiagnosisModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DiagnosisModel"/> по идентификатору
        /// </summary>
        Task<DiagnosisModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый кинотетар
        /// </summary>
        Task<DiagnosisModel> AddAsync(DiagnosisModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий кинотеатр
        /// </summary>
        Task<DiagnosisModel> EditAsync(DiagnosisModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий кинотетар
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

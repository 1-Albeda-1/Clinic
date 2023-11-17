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
    }
}

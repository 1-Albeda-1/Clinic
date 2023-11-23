using Clinic.Services.Contracts.Models;

namespace Clinic.Services.Contracts.Interface
{
    public interface IMedClinicService
    {
        /// <summary>
        /// Получить список всех <see cref="MedClinicModel"/>
        /// </summary>
        Task<IEnumerable<MedClinicModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="MedClinicModel"/> по идентификатору
        /// </summary>
        Task<MedClinicModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

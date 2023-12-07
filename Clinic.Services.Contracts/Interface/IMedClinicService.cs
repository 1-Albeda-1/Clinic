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

        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        Task<MedClinicModel> AddAsync(MedClinicModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего клиента
        /// </summary>
        Task<MedClinicModel> EditAsync(MedClinicModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

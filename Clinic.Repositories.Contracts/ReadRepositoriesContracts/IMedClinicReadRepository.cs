﻿using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="MedClinic"/>
    /// </summary>
    public interface IMedClinicReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="MedClinic"/>
        /// </summary>
        Task<IReadOnlyCollection<MedClinic>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="MedClinic"/> по идентификатору
        /// </summary>
        Task<MedClinic?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="MedClinic"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, MedClinic>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="MedClinic"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}

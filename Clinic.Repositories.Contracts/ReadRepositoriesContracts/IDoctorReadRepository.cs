﻿using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Doctor"/>
    /// </summary>
    public interface IDoctorReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Doctor"/>
        /// </summary>
        Task<IReadOnlyCollection<Doctor>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Doctor"/> по идентификатору
        /// </summary>
        Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Doctor"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Doctor>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Doctor"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}

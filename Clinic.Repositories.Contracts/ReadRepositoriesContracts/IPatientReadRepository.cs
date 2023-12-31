﻿using Clinic.Context.Contracts.Models;

namespace Clinic.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Patient"/>
    /// </summary>
    public interface IPatientReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Patient"/>
        /// </summary>
        Task<IReadOnlyCollection<Patient>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Patient"/> по идентификатору
        /// </summary>
        Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Patient"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Patient>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Patient"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}

﻿using Clinic.Context.Contracts.Models;
using System.Net.Sockets;

namespace Clinic.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="BookingAppointment"/>
    /// </summary>
    public interface IBookingAppointmentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="BookingAppointment"/>
        /// </summary>
        Task<IReadOnlyCollection<BookingAppointment>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="BookingAppointment"/> по идентификатору
        /// </summary>
        Task<BookingAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="BookingAppointment"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, BookingAppointment>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="BookingAppointment"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
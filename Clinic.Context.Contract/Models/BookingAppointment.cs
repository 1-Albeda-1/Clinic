﻿

namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Запись на приём
    /// </summary>
    public class BookingAppointment : BaseAuditEntity
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        public Guid? Patient { get; set; }

        /// <summary>
        /// ID Время приема пациента 
        /// </summary>
        public Guid? TimeTable { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string Сomplaint { get; set; } = string.Empty;
    }
}

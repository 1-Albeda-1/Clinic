﻿namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Рассписание 
    /// </summary>
    public class TimeTable : BaseAuditEntity
    {
        /// <summary>
        /// Время приема
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public int Office { get; set; }

        /// <summary>
        /// ID врача
        /// </summary>
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public ICollection<BookingAppointment> BookingAppointments { get; set; }
    }
}

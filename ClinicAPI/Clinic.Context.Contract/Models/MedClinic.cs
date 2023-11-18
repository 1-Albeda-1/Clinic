﻿using Clinic.Context.Contracts.Enums;


namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Поликлиника
    /// </summary>
    public class MedClinic : BaseAuditEntity
    {
        /// <summary>
        /// Адресс
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Название поликлиники
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

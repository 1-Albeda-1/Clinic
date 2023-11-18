﻿using Clinic.Context.Contracts.Enums;
using System.Xml.Linq;


namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Врач
    /// </summary>
    public class Doctor : BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Тип документа на работу
        /// </summary>
        public DocumentTypes DocumentType { get; set; }

        /// <summary>
        /// Категория врача
        /// </summary>
        public CategoriesTypes CategoriesTypes { get; set; }

        /// <summary>
        /// Отделение
        /// </summary>
        public Department? Department { get; set; }
    }
}

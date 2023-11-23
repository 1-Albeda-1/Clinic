using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Context.Contracts.Enums
{
    /// <summary>
    /// Тип документов
    /// </summary>
    public enum DocumentTypes
    {
        /// <summary>
        /// Не определён
        /// </summary>
        None,

        /// <summary>
        /// Пасспорт
        /// </summary>
        Passport,

        /// <summary>
        /// Свидетельство о рождении
        /// </summary>
        BirthCertificate,
    }
}

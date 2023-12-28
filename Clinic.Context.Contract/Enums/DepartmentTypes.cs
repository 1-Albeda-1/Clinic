using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Context.Contracts.Enums
{
    /// <summary>
    /// Типы отделений поликлининки
    /// </summary>
    public enum DepartmentTypes
    {
        /// <summary>
        /// Не определено
        /// </summary>
        None,

        /// <summary>
        ///  Терапевтическое
        /// </summary>
        Therapeutic,

        /// <summary>
        ///  Хирургическое
        /// </summary>
        Surgical,

        /// <summary>
        ///  Гинекологическое
        /// </summary>
        Gynecological,

        /// <summary>
        ///  Травмпункт
        /// </summary>
        Emergency,

        /// <summary>
        ///  Кардиологическое
        /// </summary>
        Cardiological,

        /// <summary>
        ///  Педиатрическое
        /// </summary>
        Pediatric
    }
}

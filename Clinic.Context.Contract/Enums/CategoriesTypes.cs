using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Context.Contracts.Enums
{
    /// <summary>
    /// Типы категорий врачей
    /// </summary>
    public enum CategoriesTypes
    {
        /// <summary>
        /// Не определено
        /// </summary>
        None,

        /// <summary>
        ///  Первая
        /// </summary>
        First,

        /// <summary>
        ///  Вторая
        /// </summary>
        Second,

        /// <summary>
        ///  Высшая
        /// </summary>
        Highest
    }
}

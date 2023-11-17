using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.API.Enums
{
    /// <summary>
    /// Типы категорий врачей
    /// </summary>
    public enum CategoriesTypesResponse
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
        Highest,
    }
}

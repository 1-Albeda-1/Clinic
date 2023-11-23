using Clinic.Context.Contracts.Enums;

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель отделений поликлиник
    /// </summary>
    public class DepartmentModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название отделения
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

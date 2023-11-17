using Clinic.Services.Contracts.Enums;

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель врача
    /// </summary>
    public class DoctorModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        /// Категория врача
        /// </summary>
        public CategoriesTypesModel CategoriesType { get; set; }

        /// <summary>
        /// Отделение
        /// </summary>
        public DepartmentTypesModel DepartmentType { get; set; }
    }
}

using Clinic.API.Enums;

namespace Clinic.API.Models.Response
{
    /// <summary>
    /// Врач
    /// </summary>
    public class DoctorResponse
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
        public CategoriesTypesResponse CategoriesType { get; set; }

        /// <summary>
        /// Отделение
        /// </summary>
        public DepartmentTypesResponse DepartmentType { get; set; }
    }
}

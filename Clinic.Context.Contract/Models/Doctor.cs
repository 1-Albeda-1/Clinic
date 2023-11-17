using Clinic.Context.Contracts.Enums;



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
        /// Категория врача
        /// </summary>
        public CategoriesTypes CategoriesType { get; set; }

        /// <summary>
        /// Отделение
        /// </summary>
        public DepartmentTypes DepartmentType { get; set; }
    }
}

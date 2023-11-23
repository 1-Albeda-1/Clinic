using Clinic.Context.Contracts.Enums;


namespace Clinic.Context.Contracts.Models
{
    /// <summary>
    /// Отделения поликлиники
    /// </summary>
    public class Department : BaseAuditEntity
    {
        /// <summary>
        /// Название отделения
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

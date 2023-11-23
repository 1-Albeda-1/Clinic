namespace Clinic.Common.EntityInterface
{
    /// <summary>
    /// Аудит удаление сущности
    /// </summary>
    public interface IEntityAuditDeleted
    {
        /// <summary>
        /// Дата удаление
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

    }
}

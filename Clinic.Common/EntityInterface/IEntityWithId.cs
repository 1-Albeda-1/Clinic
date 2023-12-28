namespace Clinic.Common.EntityInterface
{
    /// <summary>
    /// Аудит сущности с Id
    /// </summary>
    public interface IEntityWithId
    {
        public Guid Id { get; set; }
    }
}

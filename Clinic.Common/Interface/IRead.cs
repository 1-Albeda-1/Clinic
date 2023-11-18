using Clinic.Common.EntityInterface;

namespace Clinic.Common.Interface
{
    /// <summary>
    /// Интерфейс чтения БД
    /// </summary>
    public interface IRead
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary> 
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}

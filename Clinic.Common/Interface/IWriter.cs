using Clinic.Common.EntityInterface;

namespace Clinic.Common.Interface
{
    /// <summary>
    /// Интерфейс записи в БД
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
    }
}

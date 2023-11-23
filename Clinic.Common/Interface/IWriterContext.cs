namespace Clinic.Common.Interface
{
    /// <summary>
    /// Контекст для работы с записями в БД
    /// </summary>
    public interface IWriterContext
    {
        /// <inheritdoc cref="IWriter"/>
        IWriter Writer { get; }

        /// <inheritdoc cref="IUnitOfWork"/>
        IUnitOfWork UnitOfWork { get; }

        /// <inheritdoc cref="IDateTimeProvider"/>
        IDateTimeProvider DateTimeProvider { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        /// <remarks>В реальной системе с авторизацией тут будет IIdentity</remarks>
        string UserName { get; }
    }
}

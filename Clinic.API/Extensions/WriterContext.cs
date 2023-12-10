using Clinic.Common;
using Clinic.Common.Interface;

namespace Clinic.API.Extensions
{
    public class WriterContext : IWriterContext
    {
        /// <inheritdoc/>
        public IWriter Writer { get; }

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork { get; }

        /// <inheritdoc/>
        public IDateTimeProvider DateTimeProvider { get; }

        /// <inheritdoc/>
        public string UserName { get; } = "Clinic.API";

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DbWriterContext"/>
        /// </summary>
        /// <remarks>В реальном проекте надо добавлять IIdentity для доступа к
        /// информации об авторизации</remarks>
        public WriterContext(
            IWriter writer,
            IUnitOfWork unitOfWork, IDateTimeProvider provider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            DateTimeProvider = provider;
        }
    }
}

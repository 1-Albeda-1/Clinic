using Clinic.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;

namespace Clinic.Context.Tests
{
    /// <summary>
    /// Класс <see cref="ClinicContext"/> для тестов с базой в памяти. Один контекст на тест
    /// </summary>
    public abstract class ClinicContextInMemory : IAsyncDisposable
    {
        protected readonly CancellationToken CancellationToken;
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Контекст <see cref="ClinicContext"/>
        /// </summary>
        protected ClinicContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork"/>
        protected IUnitOfWork UnitOfWork => Context;

        /// <inheritdoc cref="IRead"/>
        protected IRead Reader => Context;

        protected IWriterContext WriterContext => new TestWriterContext(Context, UnitOfWork);

        protected ClinicContextInMemory()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken = cancellationTokenSource.Token;
            var optionsBuilder = new DbContextOptionsBuilder<ClinicContext>()
                .UseInMemoryDatabase($"MoneronTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new ClinicContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IDisposable"/>
        public async ValueTask DisposeAsync()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            try
            {
                await Context.Database.EnsureDeletedAsync();
                await Context.DisposeAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
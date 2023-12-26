using Microsoft.EntityFrameworkCore;
using Clinic.Common.Interface;
using Clinic.Context;
using Clinic.Context.Contracts;
using Xunit;
using Clinic.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.API.Tests.Infrastructures
{
    public class ClinicAPIFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private ClinicContext? clinicContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClinicAPIFixture"/>
        /// </summary>
        public ClinicAPIFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => ClinicContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await ClinicContext.Database.EnsureDeletedAsync();
            await ClinicContext.Database.CloseConnectionAsync();
            await ClinicContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public IClinicContext Context => ClinicContext;

        public IUnitOfWork UnitOfWork => ClinicContext;

        internal ClinicContext ClinicContext
        {
            get
            {
                if (clinicContext != null)
                {
                    return clinicContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                clinicContext = scope.ServiceProvider.GetRequiredService<ClinicContext>();
                return clinicContext;
            }
        }
    }
}

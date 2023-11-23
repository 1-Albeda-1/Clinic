using Clinic.Common.Interface;
using Clinic.Context.Contracts.Interface;
using Clinic.General;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Clinic.Context
{
    public static class RegistrationContexts
    {
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<IRead>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IWriter>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IClinicContext>(provider => provider.GetRequiredService<ClinicContext>());
        }
    }
}

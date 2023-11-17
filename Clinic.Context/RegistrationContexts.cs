using Clinic.General;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Context
{
    public static class RegistrationContexts
    {
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IContextAnchor>(ServiceLifetime.Singleton);
        }
    }
}

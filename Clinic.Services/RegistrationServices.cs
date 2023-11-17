using Clinic.General;
using Clinic.Services.Anchors;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Services
{
    public static class RegistrationServices
    {
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }
    }
}

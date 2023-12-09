using Clinic.General;
using Clinic.Repositories.Anchors;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Repositories
{
    public static class RegistrationRepositories
    {
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}

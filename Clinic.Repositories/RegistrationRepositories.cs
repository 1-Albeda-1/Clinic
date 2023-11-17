using Clinic.General;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Repositories
{
    public static class RegistrationRepositories
    {
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IReadRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}

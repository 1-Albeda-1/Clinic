using Clinic.General;
using Clinic.Repositories.Anchors;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Repositories
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class RegistrationRepositories
    {
        /// <summary>
        /// Регистрация репозиториев
        /// </summary>
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}

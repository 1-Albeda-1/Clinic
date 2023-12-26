using Clinic.General;
using Clinic.Services.Anchors;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class RegistrationServices
    {
        /// <summary>
        /// Регистрация всех сервисов и валидатора
        /// </summary>
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }
    }
}

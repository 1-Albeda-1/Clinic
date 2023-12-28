using Clinic.Common.Interface;
using Clinic.Context.Contracts.Interface;
using Clinic.General;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Clinic.Context
{
    /// <summary>
    /// Методы пасширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class RegistrationContexts
    {
        /// <summary>
        /// Регистрирует все что связано с контекстом
        /// </summary>
        /// <param name="service"></param>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<IRead>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IWriter>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IClinicContext>(provider => provider.GetRequiredService<ClinicContext>());
        }
    }
}

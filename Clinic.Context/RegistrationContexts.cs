﻿using Clinic.Common.Interface;
using Clinic.Context.Anhcors;
using Clinic.General;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Clinic.Context
{
    public static class RegistrationContexts
    {
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IContextAnchor>(ServiceLifetime.Singleton);
            service.TryAddScoped<IRead>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IWriter>(provider => provider.GetRequiredService<ClinicContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ClinicContext>());
        }
    }
}

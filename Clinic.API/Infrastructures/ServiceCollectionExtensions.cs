using AutoMapper;
using Clinic.Common;
using Clinic.Context;
using Clinic.Repositories;
using Clinic.Services;
using Clinic.Services.Automappers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Clinic.API.Infrastructures
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection service)
        {
            service.RegisterModule<ContextModule>();
            service.RegisterAssemblyInterfaceAssignableTo<IReadRepositoryAnchor>(ServiceLifetime.Scoped);
            service.RegisterAssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
        }
        public static void AddMapper(this IServiceCollection service)
        {
            var mapperConfig = new MapperConfiguration(ms =>
            {
                ms.AddProfile(new ServiceProfile());
            });
            mapperConfig.AssertConfigurationIsValid();
            var mapper = mapperConfig.CreateMapper();

            service.AddSingleton(mapper);
        }

        public static void RegisterAssemblyInterfaceAssignableTo<TInterface>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var serviceType = typeof(TInterface);
            var types = serviceType.Assembly.GetTypes()
                .Where(x => serviceType.IsAssignableFrom(x) && !(x.IsAbstract || x.IsInterface));
            foreach (var type in types)
            {
                services.TryAdd(new ServiceDescriptor(type, type, lifetime));
                var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) && i.IsPublic && i != serviceType);
                foreach (var interfaceType in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(interfaceType,
                        provider => provider.GetRequiredService(type),
                        lifetime));
                }
            }
        }

        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("BookingAppointment", new OpenApiInfo { Title = "Сущность записи на прием", Version = "v1" });
                c.SwaggerDoc("Diagnosis", new OpenApiInfo { Title = "Сущность диагнозы", Version = "v1" });
                c.SwaggerDoc("Doctor", new OpenApiInfo { Title = "Сущность врачи", Version = "v1" });
                c.SwaggerDoc("MedClinic", new OpenApiInfo { Title = "Сущность поликлиники", Version = "v1" });
                c.SwaggerDoc("Patient", new OpenApiInfo { Title = "Сущность пациенты", Version = "v1" });
                c.SwaggerDoc("TimeTable", new OpenApiInfo { Title = "Сущность рассписание", Version = "v1" });
            });
        }
        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("BookingAppointment/swagger.json", "Записи на прием");
                x.SwaggerEndpoint("Diagnosis/swagger.json", "Диагнозы");
                x.SwaggerEndpoint("Doctor/swagger.json", "Врачи");
                x.SwaggerEndpoint("MedClinic/swagger.json", "Поликлиники");
                x.SwaggerEndpoint("Patient/swagger.json", "Пациенты");
                x.SwaggerEndpoint("TimeTable/swagger.json", "Рассписание");
            });
        }

        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Clinic.Common.Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as Clinic.Common.Module;
            if (instance == null)
            {
                return;
            }
            instance.CreateModule(services);
        }
    }
}

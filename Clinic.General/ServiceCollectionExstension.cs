using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Clinic.General
{
    public static class ServiceCollectionExstension
    {
        public static void RegistrationOnInterface<TInterface>(this IServiceCollection serviceDescriptors,
            ServiceLifetime lifetime)
        {
            var servicetype = typeof(TInterface);
            var allTypes = servicetype.Assembly.GetTypes()
                .Where(x => servicetype.IsAssignableFrom(x)
                && !(x.IsInterface || x.IsAbstract));

            foreach (var type in allTypes)
            {
                serviceDescriptors.TryAdd(new ServiceDescriptor(type, type, lifetime));
                var interfaces = type.GetTypeInfo().ImplementedInterfaces
                    .Where(x => x != typeof(IDisposable) && x.IsPublic && x != servicetype);

                foreach (Type interfaceType in interfaces)
                {
                    serviceDescriptors.TryAdd(new ServiceDescriptor(interfaceType, provider =>
                    provider.GetRequiredService(type), lifetime));
                }
            }
        }
    }
}

using Clinic.Common;
using Clinic.Context.Contracts;
using Clinic.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddSingleton<IClinicContext, ClinicContext>();
        }
    }

    /// <summary>
    /// Интерфейсный маркер, для регистрации Context
    /// </summary>
    public interface IContextAnchor { };
}

using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Common
{
    public abstract class Module
    {
        /// <summary>
        /// Создаёт зависимости
        /// </summary>
        public abstract void CreateModule(IServiceCollection service);
    }
}
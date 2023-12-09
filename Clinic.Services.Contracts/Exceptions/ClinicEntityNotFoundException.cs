using Clinc.Services.Contracts.Exceptions;

namespace Clinic.Services.Contracts.Exceptions
{
    public class ClinicEntityNotFoundException<TEntity> : ClinicNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClinicEntityNotFoundException{TEntity}"/>
        /// </summary>
        public ClinicEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}

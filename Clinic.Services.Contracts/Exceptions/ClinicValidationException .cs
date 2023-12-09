using Clinic.Shared;


namespace Clinic.Services.Contracts.Exceptions
{
    public class ClinicValidationException : ClinicException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AdministrationValidationException"/>
        /// </summary>
        public ClinicValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}

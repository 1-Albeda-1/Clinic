using Clinic.Services.Contracts.Exceptions;

namespace Clinc.Services.Contracts.Exceptions
{
    public class ClinicInvalidOperationException : ClinicException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClinicInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public ClinicInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}

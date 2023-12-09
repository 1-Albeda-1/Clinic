using Clinic.Services.Contracts.Exceptions;

namespace Clinc.Services.Contracts.Exceptions
{
    public class ClinicNotFoundException : ClinicException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClinicNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public ClinicNotFoundException(string message)
            : base(message)
        { }
    }
}

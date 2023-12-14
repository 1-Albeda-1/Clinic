namespace Clinic.Services.Contracts.Exceptions
{
    public abstract class ClinicException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClinicException"/> без параметров
        /// </summary>
        protected ClinicException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClinicException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected ClinicException(string message)
            : base(message) { }
    }
}

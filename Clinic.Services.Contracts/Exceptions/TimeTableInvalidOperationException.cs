namespace Clinc.Services.Contracts.Exceptions
{
    public class TimeTableInvalidOperationException : TimeTableException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public TimeTableInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}

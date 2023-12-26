using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания расписания
    /// </summary>
    public class TimeTableRequest : CreateTimeTableRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Models.Request
{
    public class TimeTableRequest : CreateTimeTableRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}

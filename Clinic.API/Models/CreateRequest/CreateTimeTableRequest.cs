namespace Clinic.API.Models.CreateRequest
{
    public class CreateTimeTableRequest
    {
        /// <summary>
        /// Время приема
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Кабинет
        /// </summary>
        public int Office { get; set; }

        /// <summary>
        /// ID Врача
        /// </summary>
        public Guid? Doctor { get; set; }
    }
}

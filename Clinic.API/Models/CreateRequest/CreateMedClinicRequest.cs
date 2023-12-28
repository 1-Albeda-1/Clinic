namespace Clinic.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания поликлиники
    /// </summary>
    public class CreateMedClinicRequest
    {
        /// <summary>
        /// Адресс
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Название поликлиники
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

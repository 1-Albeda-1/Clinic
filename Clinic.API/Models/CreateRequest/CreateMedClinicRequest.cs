namespace Clinic.API.Models.CreateRequest
{
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

namespace Clinic.Services.Contracts.Models
{
    /// <summary>
    /// Модель поликлиники
    /// </summary>
    public class MedClinicModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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

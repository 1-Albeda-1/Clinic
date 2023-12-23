namespace Clinic.API.Models.CreateRequest
{
    public class CreatePatientRequest
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Полис
        /// </summary>
        public long Policy { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTimeOffset Birthday { get; set; }

        /// <summary>
        /// ID поликлиники
        /// </summary>
        public Guid? MedClinicId { get; set; }

        /// <summary>
        /// ID диагноза
        /// </summary>
        public Guid DiagnosisId { get; set; }
    }
}

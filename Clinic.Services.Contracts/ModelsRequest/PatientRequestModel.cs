using Clinic.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services.Contracts.ModelsRequest
{
    public class PatientRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public Guid? MedClinic { get; set; }

        /// <summary>
        /// ID диагноза
        /// </summary>
        public Guid Diagnosis { get; set; }
    }
}

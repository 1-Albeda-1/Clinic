using Clinic.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services.Contracts.ModelsRequest
{
    public class BookingAppointmentRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID клиента
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// ID время приема пациента 
        /// </summary>
        public Guid TimeTableId { get; set; }

        /// <summary>
        /// Жалоба пациента
        /// </summary>
        public string? Сomplaint { get; set; }
    }
}

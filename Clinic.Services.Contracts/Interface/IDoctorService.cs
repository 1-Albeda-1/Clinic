using Clinic.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services.Contracts.Interface
{
    public interface IDoctorService
    {
        /// <summary>
        /// Получить список всех <see cref="DoctorModel"/>
        /// </summary>
        Task<IEnumerable<DoctorModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DoctorModel"/> по идентификатору
        /// </summary>
        Task<DoctorModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

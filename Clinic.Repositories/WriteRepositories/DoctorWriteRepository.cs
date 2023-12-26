using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using System.Net.Sockets;

namespace Clinic.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IDoctorWriteRepository"/>
    /// </summary>
    public class DoctorWriteRepository : BaseWriteRepository<Doctor>, IDoctorWriteRepository, IRepositoryAnchor
    {
        public DoctorWriteRepository(IWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}

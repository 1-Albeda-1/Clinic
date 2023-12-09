using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using System.Net.Sockets;

namespace Clinic.Repositories.WriteRepositories
{
    public class MedClinicWriteRepository : BaseWriteRepository<MedClinic>, IMedClinicWriteRepository, IRepositoryAnchor
    {
        public MedClinicWriteRepository(IWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}

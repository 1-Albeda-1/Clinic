using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using System.Net.Sockets;

namespace Clinic.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="IPatientWriteRepository"/>
    /// </summary>
    public class PatientWriteRepository : BaseWriteRepository<Patient>, IPatientWriteRepository, IRepositoryAnchor
    {
        public PatientWriteRepository(IWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}

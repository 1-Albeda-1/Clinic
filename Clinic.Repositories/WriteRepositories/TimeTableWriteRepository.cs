using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;

namespace Clinic.Repositories.WriteRepositories
{
    public class TimeTableWriteRepository : BaseWriteRepository<TimeTable>, ITimeTableWriteRepository, IRepositoryAnchor
    {
        public TimeTableWriteRepository(IWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}

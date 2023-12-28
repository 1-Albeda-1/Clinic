using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Repositories.Anchors;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;

namespace Clinic.Repositories.WriteRepositories
{
    /// <summary>
    /// Реализация <see cref="ITimeTableWriteRepository"/>
    /// </summary>
    public class TimeTableWriteRepository : BaseWriteRepository<TimeTable>, ITimeTableWriteRepository, IRepositoryAnchor
    {
        public TimeTableWriteRepository(IWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}

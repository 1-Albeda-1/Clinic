using Moq;
using Clinic.Common;
using Clinic.Common.Interface;

namespace Clinic.Context.Tests
{
    internal class TestWriterContext : IWriterContext
    {
        public IWriter Writer { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IDateTimeProvider DateTimeProvider { get; }
        public string UserName { get; }

        public TestWriterContext(IWriter writer,
            IUnitOfWork unitOfWork)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
            DateTimeProvider = dateTimeProviderMock.Object;
            UserName = "UserForTests";
        }
    }
}
using Xunit;

namespace Clinic.API.Tests.Infrastructures
{
    [CollectionDefinition(nameof(ClinicAPITestCollection))]
    public class ClinicAPITestCollection
        : ICollectionFixture<ClinicAPIFixture>
    {
    }
}

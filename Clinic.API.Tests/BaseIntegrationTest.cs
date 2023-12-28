using AutoMapper;
using Clinic.API.AutoMappers;
using Clinic.API.Tests.Infrastructures;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Interface;
using Clinic.Services.Automappers;
using Xunit;

namespace Clinic.API.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(ClinicAPITestCollection))]
    public class BaseIntegrationTest
    {
        protected readonly CustomWebApplicationFactory factory;
        protected readonly IClinicContext context;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BaseIntegrationTest(ClinicAPIFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;

            Profile[] profiles = { new APIMappers(), new ServiceProfile() };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });

            mapper = config.CreateMapper();
        }
    }
}

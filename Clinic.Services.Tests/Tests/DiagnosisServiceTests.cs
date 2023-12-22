using AutoMapper;
using FluentAssertions;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinic.Context.Tests;
using Clinic.Repositories.ReadRepositories;
using Clinic.Repositories.WriteRepositories;
using Clinic.Services.Automappers;
using Clinic.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Implementations;
using Xunit;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;

namespace Clinic.Services.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IDiagnosisService"/>
    /// </summary>
    public class DiagnosisServiceTests : ClinicContextInMemory
    {
        private readonly IDiagnosisService diagnosisService;


        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DiagnosisServiceTests"/>
        /// </summary>
        public DiagnosisServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            diagnosisService = new DiagnosisService(
                new DiagnosisReadRepository(Reader),
                new DiagnosisWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper());
        }

        /// <summary>
        /// Получение диагноза по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => diagnosisService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<Diagnosis>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение диагноза по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Diagnosis();
            await Context.Diagnosises.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await diagnosisService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Medicament,
                });
        }

       
    }
}

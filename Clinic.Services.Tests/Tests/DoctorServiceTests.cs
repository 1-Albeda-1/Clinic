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
    public class DoctorServiceTests : ClinicContextInMemory
    {
        private readonly IDoctorService doctorService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DoctorServiceTests"/>
        /// </summary>

        public DoctorServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            doctorService = new DoctorService(
                new DoctorReadRepository(Reader),
                new DoctorWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение врача по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => doctorService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<ClinicEntityNotFoundException<Doctor>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение врача по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Doctor();
            await Context.Doctors.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await doctorService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Surname,
                    target.Name,
                    target.Patronymic,
                    target.CategoriesType,
                    target.DepartmentType
                });
        }
    }
}

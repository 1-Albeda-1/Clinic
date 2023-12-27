using Clinic.API.Models;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Tests.Infrastructures;
using Clinic.Services.Contracts.Models;
using Clinic.Tests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Clinic.API.Tests.Tests
{
    public class DoctorIntegrationTest : BaseIntegrationTest
    {
        public DoctorIntegrationTest(ClinicAPIFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var doctor = mapper.Map<CreateDoctorRequest>(mapper.Map<DoctorModel>(TestDataGenerator.Doctor()));

            // Act
            string data = JsonConvert.SerializeObject(doctor);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Doctor", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DoctorResponse>(resultString);

            var doctorFirst = await context.Doctors.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            doctorFirst.Should()
                .BeEquivalentTo(doctor);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var doctor = TestDataGenerator.Doctor();
            await context.Doctors.AddAsync(doctor);
            await unitOfWork.SaveChangesAsync();

            var doctorRequest = mapper.Map<DoctorResponse>(mapper.Map<DoctorModel>(TestDataGenerator.Doctor(x => x.Id = doctor.Id)));

            // Act
            string data = JsonConvert.SerializeObject(doctorRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Doctor", contextdata);

            var doctorFirst = await context.Doctors.FirstAsync(x => x.Id == doctorRequest.Id);

            // Assert           
            doctorFirst.Should()
                .BeEquivalentTo(doctorRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var doctor1 = TestDataGenerator.Doctor();
            var doctor2 = TestDataGenerator.Doctor();

            await context.Doctors.AddRangeAsync(doctor1, doctor2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Doctor/{doctor1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DoctorResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    doctor1.Id,
                    doctor1.Name,
                    doctor1.Surname,
                    doctor1.Patronymic,
                    doctor1.DepartmentType,
                    doctor1.CategoriesType
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var doctor1 = TestDataGenerator.Doctor();
            var doctor2 = TestDataGenerator.Doctor(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Doctors.AddRangeAsync(doctor1, doctor2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Doctor");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<DoctorResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == doctor1.Id)
                .And
                .NotContain(x => x.Id == doctor2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var doctor = TestDataGenerator.Doctor();
            await context.Doctors.AddAsync(doctor);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Doctor/{doctor.Id}");

            var doctorFirst = await context.Doctors.FirstAsync(x => x.Id == doctor.Id);

            // Assert
            doctorFirst.DeletedAt.Should()
                .NotBeNull();

            doctorFirst.Should()
                .BeEquivalentTo(new
                {
                    doctor.Name,
                    doctor.Surname,
                    doctor.Patronymic,
                    doctor.DepartmentType,
                    doctor.CategoriesType
                });
        }
    }
}

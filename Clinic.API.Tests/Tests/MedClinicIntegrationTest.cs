using Clinic.API.Models;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
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
    public class MedClinicIntegrationTest : BaseIntegrationTest
    {
        public MedClinicIntegrationTest(ClinicAPIFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var medClinic = mapper.Map<CreateMedClinicRequest>(mapper.Map<MedClinicModel>(TestDataGenerator.MedClinic()));

            // Act
            string data = JsonConvert.SerializeObject(medClinic);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/MedClinic", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MedClinicResponse>(resultString);

            var medClinicFirst = await context.MedClinics.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            medClinicFirst.Should()
                .BeEquivalentTo(medClinic);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var medClinic = TestDataGenerator.MedClinic();
            await context.MedClinics.AddAsync(medClinic);
            await unitOfWork.SaveChangesAsync();

            var medClinicRequest = mapper.Map<MedClinicRequest>(mapper.Map<MedClinicModel>(TestDataGenerator.MedClinic(x => x.Id = medClinic.Id)));

            // Act
            string data = JsonConvert.SerializeObject(medClinicRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/MedClinic", contextdata);

            var medClinicFirst = await context.MedClinics.FirstAsync(x => x.Id == medClinicRequest.Id);

            // Assert           
            medClinicFirst.Should()
                .BeEquivalentTo(medClinicRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var medClinic1 = TestDataGenerator.MedClinic();
            var medClinic2 = TestDataGenerator.MedClinic();

            await context.MedClinics.AddRangeAsync(medClinic1, medClinic2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/MedClinic/{medClinic1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MedClinicResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    medClinic1.Id,
                    medClinic1.Name,
                    medClinic1.Address
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var medClinic1 = TestDataGenerator.MedClinic();
            var medClinic2 = TestDataGenerator.MedClinic(x => x.DeletedAt = DateTimeOffset.Now);

            await context.MedClinics.AddRangeAsync(medClinic1, medClinic2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/MedClinic");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MedClinicResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == medClinic1.Id)
                .And
                .NotContain(x => x.Id == medClinic2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var medClinic = TestDataGenerator.MedClinic();
            await context.MedClinics.AddAsync(medClinic);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/MedClinic/{medClinic.Id}");

            var medClinicFirst = await context.MedClinics.FirstAsync(x => x.Id == medClinic.Id);

            // Assert
            medClinicFirst.DeletedAt.Should()
                .NotBeNull();

            medClinicFirst.Should()
                .BeEquivalentTo(new
                {
                    medClinic.Name,
                    medClinic.Address
                });
        }
    }
}

using FluentAssertions;
using Newtonsoft.Json;
using Clinic.API.Models;
using Clinic.API.Tests.Infrastructures;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Interface;
using Clinic.Context.Contracts.Models;
using Xunit;
using AutoMapper;
using Clinic.API.Models.CreateRequest;
using Clinic.Services.Tests;
using System.Text;
using Clinic.API.Models.Request;

namespace Clinic.API.Tests.Tests
{
    [Collection(nameof(ClinicAPITestCollection))]
    public class DiagnosisIntegrationTests : BaseIntegrationTest
    {
        public DiagnosisIntegrationTests(ClinicAPIFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var diagnosis = mapper.Map<CreateDiagnosisRequest>(TestDataGenerator.Diagnosis());

            // Act
            string data = JsonConvert.SerializeObject(diagnosis);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Cinema", contextdata);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DiagnosisResponse>(resultString);
            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    diagnosis.Name,
                    diagnosis.Medicament
                });
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var diagnosis = TestDataGenerator.Diagnosis();
            await context.Diagnosises.AddAsync(diagnosis);
            await unitOfWork.SaveChangesAsync();

            var diagnosisRequest = mapper.Map<DiagnosisRequest>(TestDataGenerator.Diagnosis(x => x.Id = diagnosis.Id));

            // Act
            string data = JsonConvert.SerializeObject(diagnosisRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Diagnosis", contextdata);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DiagnosisResponse>(resultString);
            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    diagnosis.Id
                })
                .And
                .NotBeEquivalentTo(new
                {
                    diagnosis.Name,
                    diagnosis.Medicament
                });
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var diagnosis = TestDataGenerator.Diagnosis();
            await context.Diagnosises.AddAsync(diagnosis);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Diagnosis/{diagnosis.Id}");

            // Assert
            context.Diagnosises.Should().ContainSingle(x => x.Id == diagnosis.Id && x.DeletedAt != null);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var diagnosis1 = TestDataGenerator.Diagnosis();
            var diagnosis2 = TestDataGenerator.Diagnosis();

            await context.Diagnosises.AddRangeAsync(diagnosis1, diagnosis2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Diagnosis/{diagnosis1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DiagnosisResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    diagnosis1.Name,
                    diagnosis1.Medicament
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var diagnosis1 = TestDataGenerator.Diagnosis();
            var diagnosis2 = TestDataGenerator.Diagnosis(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Diagnosises.AddRangeAsync(diagnosis1, diagnosis2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Diagnosis");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<DiagnosisResponse>>(resultString);
            result.Should()
                .NotBeNull()
                .And
                .ContainSingle(x => x.Id == diagnosis1.Id);
        }
    }
}

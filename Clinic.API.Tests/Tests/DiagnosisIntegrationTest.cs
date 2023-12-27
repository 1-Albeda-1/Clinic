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
            var diagnosis = mapper.Map<CreateDiagnosisRequest>(mapper.Map<DiagnosisModel>(TestDataGenerator.Diagnosis()));

            // Act
            string data = JsonConvert.SerializeObject(diagnosis);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Diagnosis", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DiagnosisResponse>(resultString);

            var cinemaFirst = await context.Diagnosises.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            cinemaFirst.Should()
                .BeEquivalentTo(diagnosis);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var diagnosis = TestDataGenerator.Diagnosis();
            await context.Diagnosises.AddAsync(diagnosis);
            await unitOfWork.SaveChangesAsync();

            var diagnosisRequest = mapper.Map<DiagnosisRequest>(mapper.Map<DiagnosisModel>(TestDataGenerator.Diagnosis(x => x.Id = diagnosis.Id)));

            // Act
            string data = JsonConvert.SerializeObject(diagnosisRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Diagnosis", contextdata);

            var cinemaFirst = await context.Diagnosises.FirstAsync(x => x.Id == diagnosisRequest.Id);

            // Assert           
            cinemaFirst.Should()
                .BeEquivalentTo(diagnosisRequest);
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

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    diagnosis1.Id,
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
                .Contain(x => x.Id == diagnosis1.Id)
                .And
                .NotContain(x => x.Id == diagnosis2.Id);
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

            var diagnosisFirst = await context.Diagnosises.FirstAsync(x => x.Id == diagnosis.Id);

            // Assert
            diagnosisFirst.DeletedAt.Should()
                .NotBeNull();

            diagnosisFirst.Should()
                .BeEquivalentTo(new
                {
                    diagnosis.Name,
                    diagnosis.Medicament
                });
        }
    }
}

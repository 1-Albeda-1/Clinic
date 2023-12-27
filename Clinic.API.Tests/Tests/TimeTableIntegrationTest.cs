using Clinic.API.Models;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Tests.Infrastructures;
using Clinic.Context.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;
using Clinic.Tests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Clinic.API.Tests.Tests
{
    public class TimeTableIntegrationTest : BaseIntegrationTest
    {
        private readonly Doctor doctor;

        public TimeTableIntegrationTest(ClinicAPIFixture fixture) : base(fixture)
        {
            doctor = TestDataGenerator.Doctor();

            context.Doctors.Add(doctor);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var timeTable = mapper.Map<CreateTimeTableRequest>(mapper.Map<TimeTableRequestModel>(TestDataGenerator.TimeTable()));
            timeTable.DoctorId = doctor.Id;

            // Act
            string data = JsonConvert.SerializeObject(timeTable);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/TimeTable", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TimeTableResponse>(resultString);

            var timeTableFirst = await context.TimeTables.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            timeTableFirst.Should()
                .BeEquivalentTo(timeTable);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var timeTable = TestDataGenerator.TimeTable();

            SetDependenciesOrTimeTable(timeTable);
            await context.TimeTables.AddAsync(timeTable);
            await unitOfWork.SaveChangesAsync();

            var timeTabletRequest = mapper.Map<TimeTableRequest>(mapper.Map<TimeTableRequestModel>(TestDataGenerator.TimeTable(x => x.Id = timeTable.Id)));
            SetDependenciesOrTimeTableRequestModelWithTimeTable(timeTable, timeTabletRequest);

            // Act
            string data = JsonConvert.SerializeObject(timeTabletRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/TimeTable", contextdata);

            var timeTableFirst = await context.TimeTables.FirstAsync(x => x.Id == timeTabletRequest.Id);

            // Assert           
            timeTableFirst.Should()
                .BeEquivalentTo(timeTabletRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var timeTable1 = TestDataGenerator.TimeTable();
            var timeTable2 = TestDataGenerator.TimeTable();

            SetDependenciesOrTimeTable(timeTable1);
            SetDependenciesOrTimeTable(timeTable2);

            await context.TimeTables.AddRangeAsync(timeTable1, timeTable2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/TimeTable/{timeTable1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TimeTableResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    timeTable1.Id,
                    timeTable1.Office
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var timeTable1 = TestDataGenerator.TimeTable();
            var timeTable2 = TestDataGenerator.TimeTable(x => x.DeletedAt = DateTimeOffset.Now);

            SetDependenciesOrTimeTable(timeTable1);
            SetDependenciesOrTimeTable(timeTable2);

            await context.TimeTables.AddRangeAsync(timeTable1, timeTable2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/TimeTable");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TimeTableResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == timeTable1.Id)
                .And
                .NotContain(x => x.Id == timeTable2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var timeTable = TestDataGenerator.TimeTable();

            SetDependenciesOrTimeTable(timeTable);
            await context.TimeTables.AddAsync(timeTable);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/TimeTable/{timeTable.Id}");

            var timeTableFirst = await context.TimeTables.FirstAsync(x => x.Id == timeTable.Id);

            // Assert
            timeTableFirst.DeletedAt.Should()
                .NotBeNull();

            timeTableFirst.Should()
                .BeEquivalentTo(new
                {
                    timeTable.Time,
                    timeTable.Office,
                    timeTable.DoctorId
                });
        }

        private void SetDependenciesOrTimeTable(TimeTable timeTable)
        {
            timeTable.DoctorId = doctor.Id;
        }

        private void SetDependenciesOrTimeTableRequestModelWithTimeTable(TimeTable timeTable, TimeTableRequest timeTableRequest)
        {
            timeTableRequest.DoctorId = timeTable.DoctorId;
        }
    }
}

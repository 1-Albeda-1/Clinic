using Clinic.API.Models;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Tests.Infrastructures;
using Clinic.Context.Contracts.Models;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;
using Clinic.Tests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;


namespace Clinic.API.Tests.Tests
{
    public class BookingAppointmentIntegrationTest : BaseIntegrationTest
    {
        private readonly Patient patient;
        private readonly TimeTable timeTable;

        public BookingAppointmentIntegrationTest(ClinicAPIFixture fixture) : base(fixture)
        {
            patient = TestDataGenerator.Patient();
            timeTable = TestDataGenerator.TimeTable();

            context.Patients.Add(patient);
            context.TimeTables.Add(timeTable);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var bookingAppointment = mapper.Map<CreateBookingAppointmentRequest>(mapper.Map<BookingAppointmentRequestModel>(TestDataGenerator.BookingAppointment()));
            bookingAppointment.PatientId = patient.Id;
            bookingAppointment.TimeTableId = timeTable.Id;

            // Act
            string data = JsonConvert.SerializeObject(bookingAppointment);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/BookingAppointment", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookingAppointmentResponse>(resultString);

            var cinemaFirst = await context.BookingAppointments.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            cinemaFirst.Should()
                .BeEquivalentTo(bookingAppointment);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var bookingAppointment = TestDataGenerator.BookingAppointment();

            SetDependenciesOrBookingAppointment(bookingAppointment);
            await context.BookingAppointments.AddAsync(bookingAppointment);
            await unitOfWork.SaveChangesAsync();

            var bookingAppointmentRequest = mapper.Map<BookingAppointmentRequest>(mapper.Map<BookingAppointmentRequestModel>(TestDataGenerator.BookingAppointment(x => x.Id = bookingAppointment.Id)));
            SetDependenciesOrBookingAppointmentRequestModelWithBookingAppointment(bookingAppointment, bookingAppointmentRequest);

            // Act
            string data = JsonConvert.SerializeObject(bookingAppointmentRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/BookingAppointment", contextdata);

            var bookinAppointmentFirst = await context.BookingAppointments.FirstAsync(x => x.Id == bookingAppointmentRequest.Id);

            // Assert           
            bookinAppointmentFirst.Should()
                .BeEquivalentTo(bookingAppointmentRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var bookingAppointment1 = TestDataGenerator.BookingAppointment();
            var bookingAppointment2 = TestDataGenerator.BookingAppointment();

            SetDependenciesOrBookingAppointment(bookingAppointment1);
            SetDependenciesOrBookingAppointment(bookingAppointment2);

            await context.BookingAppointments.AddRangeAsync(bookingAppointment1, bookingAppointment2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/BookingAppointment/{bookingAppointment1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookingAppointmentResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    bookingAppointment1.Id,
                    bookingAppointment1.Complaint
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var bookingAppointment1 = TestDataGenerator.BookingAppointment();
            var bookingAppointment2 = TestDataGenerator.BookingAppointment(x => x.DeletedAt = DateTimeOffset.Now);

            SetDependenciesOrBookingAppointment(bookingAppointment1);
            SetDependenciesOrBookingAppointment(bookingAppointment2);

            await context.BookingAppointments.AddRangeAsync(bookingAppointment1, bookingAppointment2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/BookingAppointment");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BookingAppointmentResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == bookingAppointment1.Id)
                .And
                .NotContain(x => x.Id == bookingAppointment2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var bookingAppointment = TestDataGenerator.BookingAppointment();

            SetDependenciesOrBookingAppointment(bookingAppointment);
            await context.BookingAppointments.AddAsync(bookingAppointment);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/BookingAppointment/{bookingAppointment.Id}");

            var ticketFirst = await context.BookingAppointments.FirstAsync(x => x.Id == bookingAppointment.Id);

            // Assert
            ticketFirst.DeletedAt.Should()
                .NotBeNull();

            ticketFirst.Should()
                .BeEquivalentTo(new
                {
                    bookingAppointment.PatientId,
                    bookingAppointment.TimeTableId,
                    bookingAppointment.Complaint
                });
        }

        private void SetDependenciesOrBookingAppointment(BookingAppointment bookingAppointment)
        {
            bookingAppointment.PatientId = patient.Id;
            bookingAppointment.TimeTableId = timeTable.Id;
        }

        private void SetDependenciesOrBookingAppointmentRequestModelWithBookingAppointment(BookingAppointment bookingAppointment, BookingAppointmentRequest bookingAppointmentRequest)
        {
            bookingAppointmentRequest.PatientId = bookingAppointment.PatientId;
            bookingAppointmentRequest.TimeTableId = bookingAppointment.TimeTableId;
        }
    }
}

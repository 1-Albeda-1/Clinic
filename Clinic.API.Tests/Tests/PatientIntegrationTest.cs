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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Clinic.API.Tests.Tests
{
    public class PatientIntegrationTest : BaseIntegrationTest
    {
        private readonly MedClinic medClinic;
        private readonly Diagnosis diagnosis;

        public PatientIntegrationTest(ClinicAPIFixture fixture) : base(fixture)
        {
            medClinic = TestDataGenerator.MedClinic();
            diagnosis = TestDataGenerator.Diagnosis();

            context.MedClinics.Add(medClinic);
            context.Diagnosises.Add(diagnosis);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var patient = mapper.Map<CreatePatientRequest>(mapper.Map<PatientRequestModel>(TestDataGenerator.Patient()));
            patient.MedClinicId = medClinic.Id;
            patient.DiagnosisId = diagnosis.Id;

            // Act
            string data = JsonConvert.SerializeObject(patient);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/Patient", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PatientResponse>(resultString);

            var patientFirst = await context.Patients.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            patientFirst.Should()
                .BeEquivalentTo(patient);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var patient = TestDataGenerator.Patient();

            SetDependenciesOrPatient(patient);
            await context.Patients.AddAsync(patient);
            await unitOfWork.SaveChangesAsync();

            var patientRequest = mapper.Map<PatientRequest>(mapper.Map<PatientRequestModel>(TestDataGenerator.Patient(x => x.Id = patient.Id)));
            SetDependenciesOrPatientRequestModelWithPatient(patient, patientRequest);

            // Act
            string data = JsonConvert.SerializeObject(patientRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Patient", contextdata);

            var patientFirst = await context.Patients.FirstAsync(x => x.Id == patientRequest.Id);

            // Assert           
            patientFirst.Should()
                .BeEquivalentTo(patientRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var patient1 = TestDataGenerator.Patient();
            var patient2 = TestDataGenerator.Patient();

            SetDependenciesOrPatient(patient1);
            SetDependenciesOrPatient(patient2);

            await context.Patients.AddRangeAsync(patient1, patient2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Patient/{patient1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PatientResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    patient1.Id,
                    patient1.Surname,
                    patient1.Name,
                    patient1.Patronymic,
                    patient1.Phone,
                    patient1.Policy,
                    patient1.Birthday
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var patient1 = TestDataGenerator.Patient();
            var patient2 = TestDataGenerator.Patient(x => x.DeletedAt = DateTimeOffset.Now);

            SetDependenciesOrPatient(patient1);
            SetDependenciesOrPatient(patient2);

            await context.Patients.AddRangeAsync(patient1, patient2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Patient");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<PatientResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == patient1.Id)
                .And
                .NotContain(x => x.Id == patient2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var patient = TestDataGenerator.Patient();

            SetDependenciesOrPatient(patient);
            await context.Patients.AddAsync(patient);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Patient/{patient.Id}");

            var patientFirst = await context.Patients.FirstAsync(x => x.Id == patient.Id);

            // Assert
            patientFirst.DeletedAt.Should()
                .NotBeNull();

            patientFirst.Should()
                .BeEquivalentTo(new
                {
                    patient.Surname,
                    patient.Name,
                    patient.Patronymic,
                    patient.Phone,
                    patient.Policy,
                    patient.Birthday,
                    patient.MedClinicId,
                    patient.DiagnosisId
                });
        }

        private void SetDependenciesOrPatient(Patient patient)
        {
            patient.MedClinicId = medClinic.Id;
            patient.DiagnosisId = diagnosis.Id;
        }

        private void SetDependenciesOrPatientRequestModelWithPatient(Patient patient, PatientRequest patientRequest)
        {
            patientRequest.MedClinicId = patient.MedClinicId;
            patientRequest.DiagnosisId = patient.DiagnosisId;
        }
    }
}

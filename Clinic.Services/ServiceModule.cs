using Clinic.Common;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<IBookingAppointmentService, BookingAppointmentService>();
            service.AddScoped<IDiagnosisService, DiagnosisService>();
            service.AddScoped<IDoctorService, DoctorService>();
            service.AddScoped<IMedClinicService, MedClinicService>();
            service.AddScoped<IPatientService, PatientService>();
            service.AddScoped<ITimeTableService, TimeTableService>();
        }
    }
    /// <summary>
    /// Интерфейсный маркер, для регистрации Service
    /// </summary>
    public interface IServiceAnchor { };
}

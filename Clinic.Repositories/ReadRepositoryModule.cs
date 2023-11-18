using Clinic.Common;
using Clinic.Repositories.Contracts;
using Clinic.Repositories.Contracts.Interface;
using Clinic.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Repositories
{
    public class ReadRepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<IBookingAppointmentReadRepository, BookingAppointmentReadRepository>();
            service.AddScoped<IDiagnosisReadRepository, DiagnosisReadRepository>();
            service.AddScoped<IDoctorReadRepository, DoctorReadRepository>();
            service.AddScoped<IMedClinicReadRepository, MedClinicReadRepository>();
            service.AddScoped<IPatientReadRepository, PatientReadRepository>();
            service.AddScoped<ITimeTableReadRepository, TimeTableReadRepository>();
        }
    }
    /// <summary>
    /// Интерфейсный маркер, для регистрации ReadRepository
    /// </summary>
    public interface IReadRepositoryAnchor { };
}

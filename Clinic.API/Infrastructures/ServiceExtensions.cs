using AutoMapper;
using Clinic.Common.Interface;
using Clinic.Common;
using Clinic.Context;
using Clinic.Repositories;
using Clinic.Services;
using Clinic.Services.Automappers;
using Microsoft.OpenApi.Models;
using Clinic.API.AutoMappers;
using Clinic.API.Infrastructures.Validator;

namespace Clinic.API.Infrastructures
{
    public static class ServiceExtensions
    {
        public static void AddDependences(this IServiceCollection service)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IWriterContext, WriterContext>();
            service.RegistrationContext();
            service.RegistrationRepository();
            service.RegistrationService();
            service.AddTransient<IApiValidatorService, ApiValidatorService>();
        }

        public static void AddMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(APIMappers), typeof(ServiceProfile));
        }
     
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("BookingAppointment", new OpenApiInfo { Title = "Сущность записи на прием", Version = "v1" });
                c.SwaggerDoc("Diagnosis", new OpenApiInfo { Title = "Сущность диагнозы", Version = "v1" });
                c.SwaggerDoc("Doctor", new OpenApiInfo { Title = "Сущность врачи", Version = "v1" });
                c.SwaggerDoc("MedClinic", new OpenApiInfo { Title = "Сущность поликлиники", Version = "v1" });
                c.SwaggerDoc("Patient", new OpenApiInfo { Title = "Сущность пациенты", Version = "v1" });
                c.SwaggerDoc("TimeTable", new OpenApiInfo { Title = "Сущность рассписание", Version = "v1" });
            });
        }
        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("BookingAppointment/swagger.json", "Записи на прием");
                x.SwaggerEndpoint("Diagnosis/swagger.json", "Диагнозы");
                x.SwaggerEndpoint("Doctor/swagger.json", "Врачи");
                x.SwaggerEndpoint("MedClinic/swagger.json", "Поликлиники");
                x.SwaggerEndpoint("Patient/swagger.json", "Пациенты");
                x.SwaggerEndpoint("TimeTable/swagger.json", "Рассписание");
            });
        }
    }
}

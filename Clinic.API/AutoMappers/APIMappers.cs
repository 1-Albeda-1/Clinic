using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Clinic.API.Enums;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.API.AutoMappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<CategoriesTypesModel, CategoriesTypesResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<DepartmentTypesModel, DepartmentTypesResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<CreateMedClinicRequest, MedClinicModel>(MemberList.Destination)
                 .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<CreateDiagnosisRequest, DiagnosisModel>(MemberList.Destination)
                 .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<CreateDoctorRequest, DoctorModel>(MemberList.Destination)
                 .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<MedClinicRequest, MedClinicModel>(MemberList.Destination).ReverseMap();
            CreateMap<DiagnosisRequest, DiagnosisModel>(MemberList.Destination).ReverseMap();
            CreateMap<DoctorRequest, DoctorModel>(MemberList.Destination).ReverseMap();
            CreateMap<PatientRequest, PatientModel>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore()).ReverseMap();

            CreateMap<PatientRequest, PatientRequestModel>(MemberList.Destination).ReverseMap();
            CreateMap<CreatePatientRequest, PatientRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<TimeTableRequest, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore()).ReverseMap();

            CreateMap<TimeTableRequest, TimeTableRequestModel>(MemberList.Destination).ReverseMap();

            CreateMap<CreateTimeTableRequest, TimeTableRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();


            CreateMap<BookingAppointmentRequest, BookingAppointmentModel>(MemberList.Destination)
                .ForMember(x => x.Patient, opt => opt.Ignore())
                .ForMember(x => x.TimeTable, opt => opt.Ignore()).ReverseMap();

            CreateMap<BookingAppointmentRequest, BookingAppointmentRequestModel>(MemberList.Destination);
            CreateMap<CreateBookingAppointmentRequest, BookingAppointmentRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();


            CreateMap<TimeTableModel, TimeTableResponse>(MemberList.Destination);
            CreateMap<MedClinicModel, MedClinicResponse>(MemberList.Destination);
            CreateMap<DoctorModel, DoctorResponse>(MemberList.Destination);
            CreateMap<DiagnosisModel, DiagnosisResponse>(MemberList.Destination);
            CreateMap<BookingAppointmentModel, BookingAppointmentResponse>(MemberList.Destination).ReverseMap();

            CreateMap<PatientModel, PatientResponse>(MemberList.Destination);

        }
    }
}

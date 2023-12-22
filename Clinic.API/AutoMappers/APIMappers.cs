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
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<CategoriesTypesModel, CategoriesTypesResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<DepartmentTypesModel, DepartmentTypesResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<CreateMedClinicRequest, MedClinicModel>(MemberList.Destination)
                 .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<CreateDiagnosisRequest, DiagnosisModel>(MemberList.Destination)
                 .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<CreateDiagnosisRequest, DoctorModel>(MemberList.Destination)
                 .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<MedClinicRequest, MedClinicModel>(MemberList.Destination);
            CreateMap<DiagnosisRequest, DiagnosisModel>(MemberList.Destination);
            CreateMap<DoctorRequest, DoctorModel>(MemberList.Destination);
            CreateMap<PatientRequest, PatientModel>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore());

            CreateMap<PatientRequest, PatientRequestModel>(MemberList.Destination);
            CreateMap<CreatePatientRequest, PatientRequestModel>(MemberList.Destination)
               .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<TimeTableRequest, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore());

            CreateMap<TimeTableRequest, TimeTableRequestModel>(MemberList.Destination);
            CreateMap<CreateTimeTableRequest, TimeTableRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());


            CreateMap<BookingAppointmentRequest, BookingAppointmentModel>(MemberList.Destination)
                .ForMember(x => x.Patient, opt => opt.Ignore())
                .ForMember(x => x.TimeTable, opt => opt.Ignore());

            CreateMap<BookingAppointmentRequest, BookingAppointmentRequestModel>(MemberList.Destination);
            CreateMap<CreateBookingAppointmentRequest, BookingAppointmentRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());


            CreateMap<TimeTableModel, TimeTableResponse>(MemberList.Destination);
            CreateMap<MedClinicModel, MedClinicResponse>(MemberList.Destination);
            CreateMap<DoctorModel, DoctorResponse>(MemberList.Destination);
            CreateMap<DiagnosisModel, DiagnosisResponse>(MemberList.Destination);
            CreateMap<BookingAppointmentModel, BookingAppointmentResponse>(MemberList.Destination);
            CreateMap<DoctorModel, DoctorResponse>(MemberList.Destination);

            CreateMap<PatientModel, PatientResponse>(MemberList.Destination);

        }
    }
}

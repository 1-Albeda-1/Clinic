using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Clinic.API.Enums;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Contracts.Models;

namespace Clinic.API.AutoMappers
{
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<CategoriesTypesModel, CategoriesTypesResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<DepartmentTypesModel, DepartmentTypesResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<MedClinicRequest, MedClinicModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<DiagnosisRequest, DiagnosisModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<DoctorRequest, DoctorModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<PatientRequest, PatientModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());
            CreateMap<TimeTableRequest, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());



            CreateMap<MedClinicRequest, MedClinicModel>(MemberList.Destination);
            CreateMap<DiagnosisRequest, DiagnosisModel>(MemberList.Destination);
            CreateMap<DoctorRequest, DoctorModel>(MemberList.Destination);
            CreateMap<PatientRequest, PatientModel>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore()); 
            CreateMap<TimeTableRequest, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore());
            CreateMap<BookingAppointmentRequest, BookingAppointmentModel>(MemberList.Destination)
                .ForMember(x => x.Patient, opt => opt.Ignore())
                .ForMember(x => x.TimeTable, opt => opt.Ignore());


            CreateMap<CreateBookingAppointmentRequest, BookingAppointmentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => Guid.NewGuid());


            CreateMap<TimeTableModel, TimeTableResponse>(MemberList.Destination);
            CreateMap<MedClinicModel, MedClinicResponse>(MemberList.Destination);
            CreateMap<DoctorModel, DoctorResponse>(MemberList.Destination);
            CreateMap<DiagnosisModel, DiagnosisResponse>(MemberList.Destination);
            CreateMap<BookingAppointmentModel, BookingAppointmentResponse>(MemberList.Destination);
            CreateMap<DoctorModel, DoctorResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.Surname} {src.Name} {src.Patronymic}"));

            CreateMap<PatientModel, PatientResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.Surname} {src.Name} {src.Patronymic}"));

        }
    }
}

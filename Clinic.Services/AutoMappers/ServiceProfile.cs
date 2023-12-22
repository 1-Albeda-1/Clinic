using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CategoriesTypes, CategoriesTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<DepartmentTypes, DepartmentTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<Diagnosis, DiagnosisModel>(MemberList.Destination).ReverseMap();
            CreateMap<Doctor, DoctorModel>(MemberList.Destination).ReverseMap();
            CreateMap<MedClinic, MedClinicModel>(MemberList.Destination).ReverseMap();

            CreateMap<Patient, PatientModel>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore()).ReverseMap();

            CreateMap<TimeTable, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore()).ReverseMap();

            CreateMap<BookingAppointment, BookingAppointmentModel>(MemberList.Destination)
               .ForMember(x => x.Patient, opt => opt.Ignore())
               .ForMember(x => x.TimeTable, opt => opt.Ignore()).ReverseMap();


            CreateMap<PatientRequestModel, Patient>(MemberList.Destination)
                .ForMember(x => x.MedClinicId, opt => opt.MapFrom(y => y.MedClinic))
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.DiagnosisId, opt => opt.MapFrom(y => y.Diagnosis))
                .ForMember(x => x.Diagnosis, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

            CreateMap<TimeTableRequestModel, TimeTable>(MemberList.Destination)
                .ForMember(x => x.DoctorId, opt => opt.MapFrom(y => y.Doctor))
                .ForMember(x => x.Doctor, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

            CreateMap<BookingAppointmentRequestModel, BookingAppointment>(MemberList.Destination)
                .ForMember(x => x.PatientId, opt => opt.MapFrom(y => y.Patient))
               .ForMember(x => x.Patient, opt => opt.Ignore())
               .ForMember(x => x.TimeTableId, opt => opt.MapFrom(y => y.TimeTable))
               .ForMember(x => x.TimeTable, opt => opt.Ignore())
               .ForMember(x => x.CreatedAt, opt => opt.Ignore())
               .ForMember(x => x.DeletedAt, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
               .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

        }
    }
}

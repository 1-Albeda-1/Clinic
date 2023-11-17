using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.Enums;

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

            CreateMap<BookingAppointment, BookingAppointmentModel>(MemberList.Destination)
                .ForMember(x => x.Patient, opt => opt.Ignore())
                .ForMember(x => x.TimeTable, opt => opt.Ignore()); ;
            CreateMap<Diagnosis, DiagnosisModel>(MemberList.Destination);
            CreateMap<Doctor, DoctorModel>(MemberList.Destination);
            CreateMap<MedClinic, MedClinicModel>(MemberList.Destination);
            CreateMap<Patient, PatientModel>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore()); ;
            CreateMap<TimeTable, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore()); ;
        }
    }
}

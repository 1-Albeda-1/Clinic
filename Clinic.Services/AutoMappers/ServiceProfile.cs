﻿using AutoMapper;
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

            CreateMap<Diagnosis, DiagnosisModel>(MemberList.Destination);
            CreateMap<Doctor, DoctorModel>(MemberList.Destination);
            CreateMap<MedClinic, MedClinicModel>(MemberList.Destination);

            CreateMap<Patient, PatientModel>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore());

            CreateMap<TimeTable, TimeTableModel>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore());

            CreateMap<BookingAppointment, BookingAppointmentModel>(MemberList.Destination)
               .ForMember(x => x.Patient, opt => opt.Ignore())
               .ForMember(x => x.TimeTable, opt => opt.Ignore());


            CreateMap<PatientRequestModel, Patient>(MemberList.Destination)
                .ForMember(x => x.MedClinic, opt => opt.Ignore())
                .ForMember(x => x.Diagnosis, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

            CreateMap<TimeTableRequestModel, TimeTable>(MemberList.Destination)
                .ForMember(x => x.Doctor, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

            CreateMap<BookingAppointmentRequestModel, BookingAppointment>(MemberList.Destination)
               .ForMember(x => x.Patient, opt => opt.Ignore())
               .ForMember(x => x.TimeTable, opt => opt.Ignore())
               .ForMember(x => x.CreatedAt, opt => opt.Ignore())
               .ForMember(x => x.DeletedAt, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
               .ForMember(x => x.UpdatedBy, opt => opt.Ignore());

        }
    }
}

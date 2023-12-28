using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.Tests.Extensions
{
    public static class TestDataGenerator
    {
        public static BookingAppointment BookingAppointment(Action<BookingAppointment>? action = null)
        {
            var item = new BookingAppointment
            {
                Complaint = $"{Guid.NewGuid():N}"
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static Diagnosis Diagnosis(Action<Diagnosis>? action = null)
        {
            var item = new Diagnosis
            {
                Name = $"{Guid.NewGuid():N}",
                Medicament = $"{Guid.NewGuid():N}"
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }
        public static Doctor Doctor(Action<Doctor>? action = null)
        {
            var item = new Doctor
            {
                Surname = $"{Guid.NewGuid():N}",
                Name = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                CategoriesType = CategoriesTypes.First,
                DepartmentType = DepartmentTypes.Pediatric
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static MedClinic MedClinic(Action<MedClinic>? action = null)
        {
            var item = new MedClinic
            {
                Address = $"{Guid.NewGuid():N}",
                Name = $"{Guid.NewGuid():N}"
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static Patient Patient(Action<Patient>? action = null)
        {
            var item = new Patient
            {
                Surname = $"{Guid.NewGuid():N}",
                Name = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Phone = $"{Random.Shared.Next(0, 9)}",
                Policy = Random.Shared.Next(0, 9),
                Birthday = DateTimeOffset.UtcNow
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static TimeTable TimeTable(Action<TimeTable>? action = null)
        {
            var item = new TimeTable
            {
                Time = DateTimeOffset.UtcNow,
                Office = Random.Shared.Next(0, 1000)
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        //public static BookingAppointmentRequestModel BookingAppointmentRequestModel(Action<BookingAppointmentRequestModel>? action = null)
        //{
        //    var item = new BookingAppointmentRequestModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Complaint = $"{Guid.NewGuid():N}"
        //    };

        //    action?.Invoke(item);
        //    return item;
        //}

        //public static DiagnosisModel DiagnosisModel(Action<DiagnosisModel>? action = null)
        //{
        //    var item = new DiagnosisModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = $"{Guid.NewGuid():N}",
        //        Medicament = $"{Guid.NewGuid():N}"
        //    };

        //    action?.Invoke(item);
        //    return item;
        //}
        //public static DoctorModel DoctorModel(Action<DoctorModel>? action = null)
        //{
        //    var item = new DoctorModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Surname = $"{Guid.NewGuid():N}",
        //        Name = $"{Guid.NewGuid():N}",
        //        Patronymic = $"{Guid.NewGuid():N}",
        //        CategoriesType = CategoriesTypesModel.First,
        //        DepartmentType = DepartmentTypesModel.Pediatric
        //    };

        //    action?.Invoke(item);
        //    return item;
        //}

        //public static MedClinicModel MedClinicModel(Action<MedClinicModel>? action = null)
        //{
        //    var item = new MedClinicModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Address = $"{Guid.NewGuid():N}",
        //        Name = $"{Guid.NewGuid():N}"
        //    };

        //    action?.Invoke(item);
        //    return item;
        //}

        //public static PatientRequestModel PatientRequestModel(Action<PatientRequestModel>? action = null)
        //{
        //    var item = new PatientRequestModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Surname = $"{Guid.NewGuid():N}",
        //        Name = $"{Guid.NewGuid():N}",
        //        Patronymic = $"{Guid.NewGuid():N}",
        //        Phone = $"{Guid.NewGuid():N}",
        //        Policy = Random.Shared.Next(0, 9),
        //        Birthday = DateTimeOffset.UtcNow
        //    };

        //    action?.Invoke(item);
        //    return item;
        //}

        //public static TimeTableRequestModel TimeTableRequestModel(Action<TimeTableRequestModel>? action = null)
        //{
        //    var item = new TimeTableRequestModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Time = DateTimeOffset.UtcNow,
        //        Office = Random.Shared.Next(0, 1000)
        //    };

        //    action?.Invoke(item);
        //    return item;
        //}
    }
}

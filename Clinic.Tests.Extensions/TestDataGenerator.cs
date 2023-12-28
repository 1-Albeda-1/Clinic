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
                Phone = $"{string.Join("",Guid.NewGuid().ToString().Take(15))}",
                Policy = Random.Shared.Next(0, 9999),
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
    }
}

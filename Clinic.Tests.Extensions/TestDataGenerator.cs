using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;

namespace Clinic.Tests.Extensions
{
    public static class TestDataGenerator
    {
        public static BookingAppointment BookingAppointment(Action<BookingAppointment>? action = null)
        {
            var item = new BookingAppointment
            {
                Id = Guid.NewGuid(),
                Complaint = $"Сomplaint{Guid.NewGuid():N}",
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static Diagnosis Diagnosis(Action<Diagnosis>? action = null)
        {
            var item = new Diagnosis
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Medicament = $"Medicament{Guid.NewGuid():N}",
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }
        public static Doctor Doctor(Action<Doctor>? action = null)
        {
            var item = new Doctor
            {
                Id = Guid.NewGuid(),
                Surname = $"LastName{Guid.NewGuid():N}",
                Name = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                CategoriesType = CategoriesTypes.None,
                DepartmentType = DepartmentTypes.None,
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static MedClinic MedClinic(Action<MedClinic>? action = null)
        {
            var item = new MedClinic
            {
                Id = Guid.NewGuid(),
                Address = $"Address{Guid.NewGuid():N}",
                Name = $"Name{Guid.NewGuid():N}",
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static Patient Patient(Action<Patient>? action = null)
        {
            var item = new Patient
            {
                Id = Guid.NewGuid(),
                Surname = $"LastName{Guid.NewGuid():N}",
                Name = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
                Policy = Random.Shared.Next(0, 9),
                Birthday = DateTimeOffset.UtcNow,
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }

        public static TimeTable TimeTable(Action<TimeTable>? action = null)
        {
            var item = new TimeTable
            {
                Id = Guid.NewGuid(),
                Time = DateTimeOffset.UtcNow,
                Office = Random.Shared.Next(0, 1000),
            };
            item.BaseAuditSetParamtrs();

            action?.Invoke(item);
            return item;
        }
    }
}

using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.Enums;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.Services.Tests
{
    public static class TestDataGenerator
    {
        public static BookingAppointment BookingAppointment(Action<BookingAppointment>? action = null)
        {
            var item = new BookingAppointment
            {
                Id = Guid.NewGuid(),
                Сomplaint = $"Сomplaint{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

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
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

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
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

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
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

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
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

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
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }
    }
}

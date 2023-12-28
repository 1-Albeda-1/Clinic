using Clinic.Common.Interface;
using Clinic.Context.Contracts.Configuration.Configurations;
using Clinic.Context.Contracts.Interface;
using Clinic.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Context
{
    public class ClinicContext : DbContext, IClinicContext, IRead, IWriter, IUnitOfWork
    {
        /// <summary>
        /// Контекст работы с БД
        /// </summary>
        /// <remarks>
        /// 1) dotnet tool install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project Clinic.Context\Clinic.Context.csproj
        /// 4) dotnet ef database update --project Clinic.Context\Clinic.Context.csproj
        /// 5) dotnet ef database update [targetMigrationName] --Clinic.Context\Clinic.Context.csproj
        /// </remarks>
        public DbSet<MedClinic> MedClinics { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Diagnosis> Diagnosises { get; set; }

        public DbSet<TimeTable> TimeTables { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<BookingAppointment> BookingAppointments { get; set; }

        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingAppointmentTypeConfiguration).Assembly);
        }

        /// <summary>
        /// Сохранение изменений в БД
        /// </summary>
        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Обновление сущностей
        /// </summary>
        void IWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;



    }
}

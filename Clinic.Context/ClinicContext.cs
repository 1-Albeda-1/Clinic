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

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        IQueryable<TEntity> IRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;



        //private readonly IList<MedClinic> medClinic;
        //private readonly IList<Doctor> doctor;
        //private readonly IList<Diagnosis> diagnosis;
        //private readonly IList<TimeTable> timeTable;
        //private readonly IList<Patient> patient;
        //private readonly IList<BookingAppointment> bookingAppointment;

        //public ClinicContext()
        //{
        //    medClinic = new List<MedClinic>();
        //    doctor = new List<Doctor>();
        //    diagnosis = new List<Diagnosis>();
        //    timeTable = new List<TimeTable>();
        //    patient = new List<Patient>();
        //    bookingAppointment = new List<BookingAppointment>();
        //    Seed();
        //}

        //IEnumerable<MedClinic> IClinicContext.MedClinic => medClinic;
        //IEnumerable<Doctor> IClinicContext.Doctor => doctor;
        //IEnumerable<Diagnosis> IClinicContext.Diagnosis => diagnosis;
        //IEnumerable<TimeTable> IClinicContext.TimeTable => timeTable;
        //IEnumerable<Patient> IClinicContext.Patient => patient;
        //IEnumerable<BookingAppointment> IClinicContext.BookingAppointment => bookingAppointment;

        //private void Seed()
        //{
        //    var medClinic1 = new MedClinic
        //    {
        //        Id = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
        //        Address = "Address1",
        //        Name = "Name1",
        //    };
        //    var doctor1 = new Doctor
        //    {
        //        Id = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
        //        Surname = "Surname1",
        //        Name = "Name1",
        //        Patronymic = "Patronymic1",
        //        CategoriesType = CategoriesTypes.First,
        //        DepartmentType = DepartmentTypes.Cardiological,
        //    };
        //    var diagnosis1 = new Diagnosis
        //    {
        //        Id = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
        //        Name = "Name1",
        //        Medicament = "Medicament1",
        //    };
        //    var timeTable1 = new TimeTable
        //    {
        //        Id = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
        //        Time = DateTime.Now,
        //        Office = 414,
        //        Doctor = doctor1.Id
        //    };
        //    var patient1 = new Patient
        //    {
        //        Id = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
        //        Surname = "FirstName1",
        //        Name = "Patronymic1",
        //        Patronymic = "Email1",
        //        Phone = "Phone1",
        //        Policy = 11111111111,
        //        Birthday = DateTime.Now,
        //        MedClinic = medClinic1.Id,
        //        Diagnosis = diagnosis1.Id,
        //    };
        //    var bookingAppointment1 = new BookingAppointment
        //    {
        //        Id = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
        //        Patient = patient1.Id,
        //        TimeTable = timeTable1.Id,
        //        Сomplaint = "Сomplaint1",
        //    };
        //    medClinic.Add(medClinic1);
        //    doctor.Add(doctor1);
        //    diagnosis.Add(diagnosis1);
        //    timeTable.Add(timeTable1);
        //    patient.Add(patient1);
        //    bookingAppointment.Add(bookingAppointment1);
        //}
    }
}

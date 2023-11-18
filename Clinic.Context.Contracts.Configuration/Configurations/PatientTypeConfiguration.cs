using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Context.Contracts.Models;


namespace Clinic.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Пациент
    /// </summary>
    public class PatientTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        void IEntityTypeConfiguration<Patient>.Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Patient).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.TimeTable).HasDatabaseName($"{nameof(Patient)}_{nameof(Patient.Title)}");
            builder.Property(x => x.C).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Сomplaint).HasDatabaseName($"{nameof(Patient)}_{nameof(Patient.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(Patient.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Context.Contracts.Models;


namespace Clinic.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Врач
    /// </summary>
    public class DoctorTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        void IEntityTypeConfiguration<Doctor>.Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Patient).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.TimeTable).HasDatabaseName($"{nameof(Doctor)}_{nameof(Doctor.Title)}");
            builder.Property(x => x.C).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Сomplaint).HasDatabaseName($"{nameof(Doctor)}_{nameof(Doctor.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(Doctor.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}

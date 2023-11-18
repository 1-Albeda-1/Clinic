using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Context.Contracts.Models;

namespace Clinic.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Поликлиника
    /// </summary>
    public class MedClinicTypeConfiguration : IEntityTypeConfiguration<MedClinic>
    {
        void IEntityTypeConfiguration<MedClinic>.Configure(EntityTypeBuilder<MedClinic> builder)
        {
            builder.ToTable("MedClinics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Patient).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.TimeTable).HasDatabaseName($"{nameof(MedClinic)}_{nameof(MedClinic.Title)}");
            builder.Property(x => x.C).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Сomplaint).HasDatabaseName($"{nameof(MedClinic)}_{nameof(MedClinic.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(MedClinic.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}

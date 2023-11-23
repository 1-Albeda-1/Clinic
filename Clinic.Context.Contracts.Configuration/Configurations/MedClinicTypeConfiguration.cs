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
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasDatabaseName($"{nameof(MedClinic)}_{nameof(MedClinic.Name)}")
                .HasFilter($"{nameof(MedClinic.DeletedAt)} is null");
            builder.Property(x =>x.Address).HasMaxLength(200).IsRequired();
            builder.HasMany(x => x.Patients).WithOne(x => x.MedClinic).HasForeignKey(x => x.MedClinicId);
        }
    }
}

using Clinic.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Context.Contracts.Configuration.Configurations
{
    //// <summary>
    /// Диагноз
    /// </summary>
    public class DiagnosisTypeConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        void IEntityTypeConfiguration<Diagnosis>.Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.ToTable("Diagnosis");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasDatabaseName($"{nameof(Diagnosis)}_{nameof(Diagnosis.Name)}")
                .HasFilter($"{nameof(Diagnosis.DeletedAt)} is null");
            builder.Property(x => x.Medicament).HasMaxLength(50).IsRequired();
            builder.HasMany(x => x.Patients).WithOne(x => x.Diagnosis).HasForeignKey(x => x.DiagnosisId);
        }
    }
}

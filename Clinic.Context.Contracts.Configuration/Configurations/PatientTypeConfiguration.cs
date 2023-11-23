using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Context.Contracts.Models;
using System;


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
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(16).IsRequired();
            builder.HasIndex(x => x.Phone)
                .IsUnique()
                .HasDatabaseName($"{nameof(Patient)}_{nameof(Patient.Phone)}")
                .HasFilter($"{nameof(Patient.DeletedAt)} is null");

            builder.Property(x => x.Policy).HasMaxLength(16).IsRequired();
            builder.HasIndex(x => x.Policy)
                .IsUnique()
                .HasDatabaseName($"{nameof(Patient)}_{nameof(Patient.Policy)}")
                .HasFilter($"{nameof(Patient.DeletedAt)} is null");

            builder.Property(x => x.Birthday).IsRequired();
            builder.Property(x => x.DiagnosisId).IsRequired();
            builder.HasMany(x => x.BookingAppointments).WithOne(x => x.Patient).HasForeignKey(x => x.PatientId);
        }
    }
}

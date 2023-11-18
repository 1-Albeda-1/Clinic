using Clinic.Context.Contracts.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Clinic.Context.Contracts.Models;

namespace Clinic.Context.Contracts.Configuration.Configurations
{
    //// <summary>
    /// Диагноз
    /// </summary>
    public class DiagnosisTypeConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        void IEntityTypeConfiguration<Diagnosis>.Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.ToTable("BookingAppointments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Patient).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.TimeTable).HasDatabaseName($"{nameof(Diagnosis)}_{nameof(Diagnosis.Title)}");
            builder.Property(x => x.C).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Сomplaint).HasDatabaseName($"{nameof(Diagnosis)}_{nameof(Diagnosis.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(Diagnosis.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}

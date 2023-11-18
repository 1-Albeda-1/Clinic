using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Context.Contracts.Models;

namespace Clinic.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Рассписание 
    /// </summary>
    public class TimeTableTypeConfiguration : IEntityTypeConfiguration<TimeTable>
    {
        void IEntityTypeConfiguration<TimeTable>.Configure(EntityTypeBuilder<TimeTable> builder)
        {
            builder.ToTable("TimeTables");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Patient).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.TimeTable).HasDatabaseName($"{nameof(TimeTable)}_{nameof(TimeTable.Title)}");
            builder.Property(x => x.C).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Сomplaint).HasDatabaseName($"{nameof(TimeTable)}_{nameof(TimeTable.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(TimeTable.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}

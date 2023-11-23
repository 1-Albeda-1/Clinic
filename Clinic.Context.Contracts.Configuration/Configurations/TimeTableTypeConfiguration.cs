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
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.Office).IsRequired();
            builder.Property(x => x.DoctorId).IsRequired();
            builder.HasMany(x => x.BookingAppointments).WithOne(x => x.TimeTable).HasForeignKey(x => x.TimeTableId);
        }
    }
}

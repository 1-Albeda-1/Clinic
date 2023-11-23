using Clinic.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Запись на приём
    /// </summary>
    public class BookingAppointmentTypeConfiguration : IEntityTypeConfiguration<BookingAppointment>
    {
        void IEntityTypeConfiguration<BookingAppointment>.Configure(EntityTypeBuilder<BookingAppointment> builder)
        {
            builder.ToTable("BookingAppointments");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.TimeTableId).IsRequired();
        }
    }
}

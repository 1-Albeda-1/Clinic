using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinic.Context.Contracts.Models;
using System.Net.Sockets;

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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Patient).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.TimeTable).HasDatabaseName($"{nameof(BookingAppointment)}_{nameof(BookingAppointment.Title)}");
            builder.Property(x => x.C).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Сomplaint).HasDatabaseName($"{nameof(BookingAppointment)}_{nameof(BookingAppointment.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(BookingAppointment.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}

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
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CategoriesType).IsRequired();
            builder.Property(x => x.DepartmentType).IsRequired();
            builder.HasMany(x => x.TimeTables).WithOne(x => x.Doctor).HasForeignKey(x => x.DoctorId);
        }
    }
}

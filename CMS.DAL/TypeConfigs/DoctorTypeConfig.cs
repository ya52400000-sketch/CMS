using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CMS.DAL;

public class DoctorTypeConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> modelBuilder)
    {
        modelBuilder.HasKey(P => P.Id);
        modelBuilder.Property(P => P.Name).IsRequired().HasMaxLength(100);

        // 🔸 1:M Doctor - Appointment
        modelBuilder
            .HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(D => D.AppUser)
            .WithOne()
            .HasForeignKey<Doctor>(D => D.UserId);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CMS.DAL;

public class PatientTypeConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> modelBuilder)
    {
        modelBuilder.HasKey(P => P.Id);
        modelBuilder.Property(P => P.Name).IsRequired().HasMaxLength(100);

        // 🔸 1:1 Patient - MedicalRecord
        modelBuilder
            .HasOne(p => p.MedicalRecord)
            .WithOne(m => m.Patient)
            .HasForeignKey<MedicalRecord>(m => m.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔸 1:M Patient - Appointment
        modelBuilder
            .HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(P => P.AppUser)
            .WithOne()
            .HasForeignKey<Patient>(P => P.UserId);
    }

}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.DAL;

public class MedicalRecordTypeConfig : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> modelBuilder)
    {
        modelBuilder.HasKey(P => P.Id);
        modelBuilder.Property(P => P.Diagnosis).IsRequired().HasMaxLength(100);
    }
}

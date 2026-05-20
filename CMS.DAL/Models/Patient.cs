namespace CMS.DAL;

public class Patient : BaseModel<Guid>
{
    public string Name { get; set; } = string.Empty;

    // FK
    public string UserId { get; set; } = string.Empty;

    // Navigation
    public virtual AppUser AppUser { get; set; } = null!;

    public virtual MedicalRecord? MedicalRecord { get; set; }

    public virtual ICollection<Appointment> Appointments
    { get; set; } = new List<Appointment>();
}
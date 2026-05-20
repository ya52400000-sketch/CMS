namespace CMS.DAL;

public class Doctor : BaseModel<Guid>
{
    public string Name { get; set; } = string.Empty;

    // FK
    public string UserId { get; set; } = string.Empty;

    // Navigation
    public virtual AppUser AppUser { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments
    { get; set; } = new List<Appointment>();
}
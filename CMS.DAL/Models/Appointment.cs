namespace CMS.DAL;

public class Appointment : BaseModel<Guid>
{
    public DateTime Date { get; set; }

    // FKs
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }

    // Navigation
    public virtual Doctor Doctor { get; set; }
    public virtual Patient Patient { get; set; }
}
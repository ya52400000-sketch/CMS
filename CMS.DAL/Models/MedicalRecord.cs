namespace CMS.DAL;

public class MedicalRecord : BaseModel<Guid>
{
    public string Diagnosis { get; set; } //> التشخيص

    // FK
    public Guid PatientId { get; set; }

    // Navigation
    public virtual Patient Patient { get; set; }
}
namespace CMS.BLL;

public class MedicalRecordDto
{
    public Guid Id { get; set; }

    public string Diagnosis { get; set; }

    public Guid PatientId { get; set; }

    public string PatientName { get; set; }
}

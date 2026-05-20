namespace CMS.BLL;

public class CreateMedicalRecordDto
{
    public string Diagnosis { get; set; }

    public Guid PatientId { get; set; }
}

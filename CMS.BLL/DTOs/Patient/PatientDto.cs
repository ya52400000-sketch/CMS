namespace CMS.BLL;

public class PatientDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Diagnosis { get; set; }
}
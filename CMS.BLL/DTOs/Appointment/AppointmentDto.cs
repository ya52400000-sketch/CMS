namespace CMS.BLL;

public class AppointmentDto
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid DoctorId { get; set; }

    public string DoctorName { get; set; }

    public Guid PatientId { get; set; }

    public string PatientName { get; set; }
}

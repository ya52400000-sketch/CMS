namespace CMS.BLL;

public class CreateAppointmentDto
{
    public DateTime Date { get; set; }

    public Guid DoctorId { get; set; }

    public Guid PatientId { get; set; }
}

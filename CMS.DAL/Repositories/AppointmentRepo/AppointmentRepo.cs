namespace CMS.DAL;

public class AppointmentRepo : GenericRepo<Appointment>, IAppointmentRepo
{
    public AppointmentRepo(AppDbContext context) : base(context)
    {

    }
}

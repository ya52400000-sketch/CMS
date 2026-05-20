namespace CMS.DAL;

public class DoctorRepo : GenericRepo<Doctor>, IDoctorRepo
{
    public DoctorRepo(AppDbContext context) : base(context)
    {

    }
}

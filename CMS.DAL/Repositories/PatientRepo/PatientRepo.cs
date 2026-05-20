namespace CMS.DAL;

public class PatientRepo : GenericRepo<Patient>, IPatientRepo
{
    public PatientRepo(AppDbContext context) : base(context)
    {

    }
}

namespace CMS.DAL;

public class MedicalRecordRepo : GenericRepo<MedicalRecord>, IMedicalRecordRepo
{
    public MedicalRecordRepo(AppDbContext context) : base(context)
    {

    }
}

namespace CMS.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericRepo<Patient> Patients { get; }

    public IGenericRepo<Appointment> Appointments { get; }

    public IGenericRepo<Doctor> Doctors { get; }

    public IGenericRepo<MedicalRecord> MedicalRecords { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Patients = new GenericRepo<Patient>(_context);
        Appointments = new GenericRepo<Appointment>(_context);
        Doctors = new GenericRepo<Doctor>(_context);
        MedicalRecords = new GenericRepo<MedicalRecord>(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

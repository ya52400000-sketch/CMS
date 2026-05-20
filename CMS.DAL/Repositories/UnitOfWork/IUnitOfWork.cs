namespace CMS.DAL;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepo<Patient> Patients { get; }

    public IGenericRepo<Appointment> Appointments { get; }

    public IGenericRepo<Doctor> Doctors { get; }

    public IGenericRepo<MedicalRecord> MedicalRecords { get; }

    public Task<int> SaveChangesAsync();
}

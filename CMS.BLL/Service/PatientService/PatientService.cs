using CMS.DAL;

namespace CMS.BLL;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CommonResponse> AddAsync(CreatePatientDto dto)
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid(),

            Name = dto.Name,

            MedicalRecord = new MedicalRecord
            {
                Id = Guid.NewGuid(),
                Diagnosis = dto.Diagnosis
            }
        };

        await _unitOfWork.Patients.AddAsync(patient);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("Added successfully", true);
    }

    public async Task<CommonResponse> DeleteAsync(Guid id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);

        if (patient == null)
            return new CommonResponse("patient doesn't exist", false);

        _unitOfWork.Patients.Delete(patient);

        await _unitOfWork.SaveChangesAsync();

        return new CommonResponse("deleted successfully", true);
    }

    public async Task<IEnumerable<PatientDto>> GetAllAsync(Query query)
    {
        var patients = await _unitOfWork.Patients.GetAllAsync(
            query,
            p => p.MedicalRecord! //> patient.inlclude(medicalRecorde)
        );

        var result = patients.Select(p => new PatientDto
        {
            Id = p.Id,
            Name = p.Name,
            Diagnosis = p.MedicalRecord?.Diagnosis
        });

        return result;
    }

    public async Task<PatientDto?> GetByIdAsync(Guid id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(
            id,
            p => p.MedicalRecord!
        );

        if (patient == null)
            return null;

        return new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Diagnosis = patient.MedicalRecord?.Diagnosis
        };
    }

    public async Task<CommonResponse> UpdateAsync(Guid id, UpdatePatientDto dto)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(
            id,
            p => p.MedicalRecord!
        );

        if (patient == null)
            return new CommonResponse("patient doesn't exist", false);

        patient.Name = dto.Name;

        if (patient.MedicalRecord != null)
        {
            patient.MedicalRecord.Diagnosis = dto.Diagnosis;
        }

        _unitOfWork.Patients.Update(patient);

        await _unitOfWork.SaveChangesAsync();

        return new CommonResponse("updated successfully", true);
    }
}

using CMS.DAL;

namespace CMS.BLL;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IUnitOfWork _unitOfWork;
    public MedicalRecordService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CommonResponse> AddAsync(CreateMedicalRecordDto dto)
    {
        var medicalRecord = new MedicalRecord
        {
            Id = Guid.NewGuid(),

            Diagnosis = dto.Diagnosis,
            PatientId = dto.PatientId
        };

        await _unitOfWork.MedicalRecords.AddAsync(medicalRecord);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("Added Successfully", true);
    }

    public async Task<CommonResponse> DeleteAsync(Guid id)
    {
        var medicalRecord = await _unitOfWork.MedicalRecords.GetByIdAsync(id);

        if (medicalRecord == null)
            return new CommonResponse("not found", false);

        _unitOfWork.MedicalRecords.Delete(medicalRecord);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("deleted successfully", true);
    }

    public async Task<IEnumerable<MedicalRecordDto>> GetAllAsync(Query query)
    {
        var medicalRecords = await _unitOfWork.MedicalRecords.GetAllAsync(
            query,
            m => m.Patient
        );

        var result = medicalRecords.Select(m => new MedicalRecordDto
        {
            Id = m.Id,
            Diagnosis = m.Diagnosis,
            PatientId = m.PatientId,
            PatientName = m.Patient.Name
        });

        return result;
    }

    public async Task<MedicalRecordDto?> GetByIdAsync(Guid id)
    {
        var medicalRecord = await _unitOfWork.MedicalRecords.GetByIdAsync(
            id,
            m => m.Patient
        );

        if (medicalRecord == null)
            return null;

        return new MedicalRecordDto
        {
            Id = medicalRecord.Id,
            Diagnosis = medicalRecord.Diagnosis,
            PatientId = medicalRecord.PatientId,
            PatientName = medicalRecord.Patient?.Name ?? ""
        };
    }

    public async Task<CommonResponse> UpdateAsync(Guid id, UpdateMedicalRecordDto dto)
    {
        var medicalRecord = await _unitOfWork.MedicalRecords.GetByIdAsync(id);

        if (medicalRecord == null)
            return new CommonResponse("not found", false);

        medicalRecord.Diagnosis = dto.Diagnosis;

        _unitOfWork.MedicalRecords.Update(medicalRecord);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("updated succesffully", true);
    }
}

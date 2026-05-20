using CMS.DAL;

namespace CMS.BLL;

public interface IMedicalRecordService
{
    Task<IEnumerable<MedicalRecordDto>> GetAllAsync(Query query);

    Task<MedicalRecordDto?> GetByIdAsync(Guid id);

    Task<CommonResponse> AddAsync(CreateMedicalRecordDto dto);

    Task<CommonResponse> UpdateAsync(Guid id, UpdateMedicalRecordDto dto);

    Task<CommonResponse> DeleteAsync(Guid id);
}

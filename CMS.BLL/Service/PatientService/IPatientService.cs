using CMS.DAL;

namespace CMS.BLL;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllAsync(Query query);

    Task<PatientDto?> GetByIdAsync(Guid id);

    Task<CommonResponse> AddAsync(CreatePatientDto dto);

    Task<CommonResponse> UpdateAsync(Guid id, UpdatePatientDto dto);

    Task<CommonResponse> DeleteAsync(Guid id);
}

using CMS.DAL;

namespace CMS.BLL;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAllAsync(Query query);

    Task<DoctorDto?> GetByIdAsync(Guid id);

    Task<CommonResponse> AddAsync(CreateDoctorDto dto);

    Task<CommonResponse> UpdateAsync(Guid id, UpdateDoctorDto dto);

    Task<CommonResponse> DeleteAsync(Guid id);
}

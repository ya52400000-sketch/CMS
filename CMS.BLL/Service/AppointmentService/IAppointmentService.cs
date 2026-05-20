using CMS.DAL;

namespace CMS.BLL;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAsync(Query query);

    Task<AppointmentDto?> GetByIdAsync(Guid id);

    Task<CommonResponse> AddAsync(CreateAppointmentDto dto);

    Task<CommonResponse> UpdateAsync(Guid id, UpdateAppointmentDto dto);

    Task<CommonResponse> DeleteAsync(Guid id);
}

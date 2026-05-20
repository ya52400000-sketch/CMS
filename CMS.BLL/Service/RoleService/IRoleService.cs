namespace CMS.BLL;

public interface IRoleService
{
    Task<CommonResponse> AddAsync(string roleName);
    Task<CommonResponse> RemoveAsync(string roleName);
    Task<IEnumerable<string>> GetRolesAsync();
}

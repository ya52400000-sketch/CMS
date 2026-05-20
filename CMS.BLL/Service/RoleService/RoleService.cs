using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS.BLL;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<CommonResponse> AddAsync(string roleName)
    {
        var roleExist = await _roleManager.FindByNameAsync(roleName);
        if (roleExist is not null)
        {
            return new CommonResponse("role already exist", false);
        }

        var addResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (!addResult.Succeeded)
        {
            var errors = addResult.Errors.Select(e => e.Description).ToList();
            return new CommonResponse("can't create role currently", false, errors);
        }

        return new CommonResponse("role created successfully", true);
    }

    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        return await _roleManager.Roles.Select(r => r.Name ?? "").ToListAsync();
    }

    public async Task<CommonResponse> RemoveAsync(string roleName)
    {
        var roleExist = await _roleManager.FindByNameAsync(roleName);
        if (roleExist is null)
        {
            return new CommonResponse("role not found", false);
        }

        var removeResult = await _roleManager.DeleteAsync(roleExist);
        if (!removeResult.Succeeded)
        {
            var errors = removeResult.Errors.Select(e => e.Description).ToList();
            return new CommonResponse("can't create role currently", false, errors);
        }

        return new CommonResponse("role removed successfully", true);
    }
}

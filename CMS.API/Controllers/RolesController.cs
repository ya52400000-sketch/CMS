using CMS.BLL;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API;


[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<ActionResult<CommonResponse>> Create(AddRoleDto dto)
    {
        var response = await _roleService.AddAsync(dto.Name);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{roleName}")]
    public async Task<ActionResult<CommonResponse>> Remove(string roleName)
    {
        var response = await _roleService.RemoveAsync(roleName);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetAll()
    {
        return Ok(await _roleService.GetRolesAsync());
    }
}

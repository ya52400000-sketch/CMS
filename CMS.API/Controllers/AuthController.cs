using CMS.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("RegisterAdmin")]
    public async Task<ActionResult<CommonResponse>> RegisterAdmin(RegisterDto dto)
    {
        //> TODO: secure endpoint with key
        var response = await _authService.RegisterAdminAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("RegisterPatient")]
    public async Task<ActionResult<CommonResponse>> RegisterPatient(RegisterDto dto)
    {
        var response = await _authService.RegisterPatientAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("RegisterDoctor")]
    public async Task<ActionResult<CommonResponse>> RegisterDoctor(RegisterDto dto)
    {
        var response = await _authService.RegisterDoctorAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpPost("Login")]
    public async Task<ActionResult<CommonResponse>> Login(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

}

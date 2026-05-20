using CMS.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CMS.BLL;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<AppUser> userManager,
        IConfiguration configuration,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    // ========================= LOGIN =========================

    public async Task<CommonResponse> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            return new CommonResponse("Email or password is invalid", false);

        var passwordValid =
            await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
            return new CommonResponse("Email or password is invalid", false);

        var token =
            await TokenHandler.CreateTokenAsync(user, _configuration, _userManager);

        return new CommonResponse("Login successful", true, null!, token);
    }

    // ========================= REGISTER =========================

    public async Task<CommonResponse> RegisterAdminAsync(RegisterDto dto)
    {
        return await RegisterUserAsync(dto, "Admin");
    }

    public async Task<CommonResponse> RegisterDoctorAsync(RegisterDto dto)
    {
        return await RegisterUserAsync(
            dto,
            "Doctor",
            async (user) =>
            {
                var doctor = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Name = user.FullName,
                    UserId = user.Id
                };

                await _unitOfWork.Doctors.AddAsync(doctor);
            });
    }

    public async Task<CommonResponse> RegisterPatientAsync(RegisterDto dto)
    {
        return await RegisterUserAsync(
            dto,
            "Patient",
            async (user) =>
            {
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    Name = user.FullName,
                    UserId = user.Id
                };

                await _unitOfWork.Patients.AddAsync(patient);
            });
    }

    // ========================= PRIVATE METHODS =========================

    private async Task<CommonResponse> RegisterUserAsync(
    RegisterDto dto,
    string role,
    Func<AppUser, Task>? additionalAction = null)
{
    var userExist = await _userManager.FindByEmailAsync(dto.Email);

    if (userExist is not null)
    {
        return new CommonResponse("Email already exists", false);
    }

    var user = new AppUser
    {
        FullName = dto.FullName,
        Email = dto.Email,
        UserName = dto.Email
    };

    try
    {
        // Create User
        var createResult =
            await _userManager.CreateAsync(user, dto.Password);

        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors
                .Select(e => e.Description)
                .ToList();

            return new CommonResponse(
                "Registration failed",
                false,
                errors);
        }

        // Add Role
        var roleResult =
            await _userManager.AddToRoleAsync(user, role);

        if (!roleResult.Succeeded)
        {
            // rollback user
            await _userManager.DeleteAsync(user);

            var errors = roleResult.Errors
                .Select(e => e.Description)
                .ToList();

            return new CommonResponse(
                "Failed to assign role",
                false,
                errors);
        }

        // Additional Logic
        if (additionalAction is not null)
        {
            await additionalAction(user);

            await _unitOfWork.SaveChangesAsync();
        }

        return new CommonResponse(
            "Registered successfully",
            true);
    }
    catch (Exception ex)
    {
        // rollback created user
        var createdUser =
            await _userManager.FindByEmailAsync(dto.Email);

        if (createdUser is not null)
        {
            await _userManager.DeleteAsync(createdUser);
        }

        return new CommonResponse(
            "There is a problem during registration",
            false,
            new List<string> { ex.Message });
    }
}
}

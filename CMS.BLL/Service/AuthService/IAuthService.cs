namespace CMS.BLL;

public interface IAuthService
{
    Task<CommonResponse> RegisterAdminAsync(RegisterDto dto);
    Task<CommonResponse> RegisterPatientAsync(RegisterDto dto);
    Task<CommonResponse> RegisterDoctorAsync(RegisterDto dto);
    Task<CommonResponse> LoginAsync(LoginDto dto);
}

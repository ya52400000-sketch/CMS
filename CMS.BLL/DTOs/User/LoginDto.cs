using System.ComponentModel.DataAnnotations;

namespace CMS.BLL;

public class LoginDto
{
    [Required]
    [EmailAddress(ErrorMessage = "enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

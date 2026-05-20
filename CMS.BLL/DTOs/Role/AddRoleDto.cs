using System.ComponentModel.DataAnnotations;

namespace CMS.BLL;

public class AddRoleDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "the name must be less than 50 char")]
    public string Name { get; set; } = string.Empty;
}

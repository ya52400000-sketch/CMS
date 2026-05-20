using Microsoft.AspNetCore.Identity;

namespace CMS.DAL;

//> delete roles
//> create Admin
//> create Patient role
//> create Doctor role
public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}


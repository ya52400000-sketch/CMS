using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CMS.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CMS.BLL;

public class TokenHandler
{   
    public static async Task<string> CreateTokenAsync(AppUser user, IConfiguration configuration, UserManager<AppUser> userManager)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var userRoles = await userManager.GetRolesAsync(user);
        foreach(var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var keyInBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var key = new SymmetricSecurityKey(keyInBytes);

        var _issuer = configuration["Jwt:Issuer"];
        var _audience = configuration["Jwt:Audience"];
        var _expireTime = configuration["Jwt:DurationInMinutes"];
        var _cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_expireTime)),
            signingCredentials: _cred,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

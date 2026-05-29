using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NutriFacts.Auth;
public class JwtService
{
    public string GenerateToken(AppUser user)
    {
        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("a9F!2kL8pQ7xZ1mV3sD6rT9yW4nB8cX0"));//sensitive stuff 

        var creds =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public class TokenService (IConfiguration config): ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot accsess tokenKey from appsettings");
        if(tokenKey.Length < 64) throw new Exception("Your tokenKey need to be longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        // you should install package from nuget to use this methodSymmetricSecurityKey
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier , user.Name)
        };

    var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds

        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
    return tokenHandler.WriteToken(token);
    }
}
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgentRestApi.Services
{
    //public class JwtService(IConfiguration configuration) :IJwtService
    //{

    //    public string GenerateToken()
    //    {
    //        string key = configuration.GetValue("Jwt:Key", string.Empty)
    //            ?? throw new ArgumentNullException(nameof(configuration));

    //        int expiry = configuration.GetValue("Jwt: ExpiryInMinutes", 60);

    //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //        var claims = new[]
    //                {
    //            new Claim(ClaimTypes.),
    //            new Claim(ClaimTypes.)
    //        };

    //        var token = new JwtSecurityToken(
    //            issuer: configuration["Jwt:Issuer"],
    //            audience: configuration["Jwt:Audience"],
    //            expires: DateTime.Now.AddMinutes(expiry),
    //            claims: claims,
    //            signingCredentials: credentials);

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
}

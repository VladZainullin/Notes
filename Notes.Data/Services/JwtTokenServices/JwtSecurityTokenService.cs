using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notes.Core.Entities;

namespace Notes.Data.Services.JwtTokenServices;

public sealed class JwtSecurityTokenService
{
    private readonly IConfiguration _configuration;

    public JwtSecurityTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Create(User user)
    {
        var claims = CreateClaims(user);

        var securityKey = GetSecurityKey();

        var signingCredentials = GetSigningCredentials(securityKey);

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentications:Issuer"],
            _configuration["Authentications:Audience"],
            claims,
            DateTime.Now,
            DateTime.Now.AddHours(1),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        ;
    }

    private static SigningCredentials GetSigningCredentials(SecurityKey securityKey)
    {
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        return signingCredentials;
    }

    private SymmetricSecurityKey GetSecurityKey()
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentications:Security"]));

        return symmetricSecurityKey;
    }

    private static IEnumerable<Claim> CreateClaims(User user)
    {
        var claims = new Claim[]
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
            new("Password", user.Password!)
        };

        return claims;
    }
}
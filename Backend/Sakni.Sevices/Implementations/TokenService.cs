using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sakani.Data.Models;
using Sakni.Sevices.Interfaces;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _secretKey = _configuration["Jwt:SecretKey"];
        _issuer = _configuration["Jwt:Issuer"];
        _audience = _configuration["Jwt:Audience"];
    }

    public async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = _audience,
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false
        };

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(token);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}

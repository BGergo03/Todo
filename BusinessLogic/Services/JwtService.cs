using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly byte[] key;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        key = Encoding.UTF8.GetBytes("be8c76dc3e827a185767fcc2fcb7a1a3733221dc47044afc892e303f041f813798a2d9ff0629e0faa19b0b6e84f62ca262210e543e21f7906abb95b11e95d6b2a5ffd8d5e1acdede7723a9614739a755d84d4f4a7c60821733047c5ef86e2dbe2831d96e5a226b0a7a0a73481ff8499a1f0bc7c33527e46b9becd14e085142f5764921de0a1a42ad4d5b2f74b877933bdecd395aab374210116b338ad40b027bb1ad48fac53d76f3db0f341fe8b3bb842d602acb9d4dbb6c0901733d132f9dca0f244b31672505fc0d6bf8ac2d890adef11369ec134c322167c896ac12478c2efc1d0cbefa9fe3dca179d62f40b0ef4fb3c03fce6e5782b81abe508c341182e0");
    }

    public string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(12),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<ClaimsPrincipal> ValidateTokenAsync(string token, CancellationToken cancellationToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
    }
}
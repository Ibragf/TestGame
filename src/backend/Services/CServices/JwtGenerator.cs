using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Settings;

namespace Services.CServices;

public interface IJwtGenerator
{
    public string Generate(IEnumerable<Claim> claims);
}

public sealed class JwtGenerator : IJwtGenerator
{
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(
        JwtSecurityTokenHandler jwtTokenHandler,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _jwtTokenHandler = jwtTokenHandler;
    }

    public string Generate(IEnumerable<Claim> claims)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var utcNow = DateTimeOffset.UtcNow.DateTime;

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            notBefore: utcNow,
            expires: utcNow.AddDays(1),
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

        return _jwtTokenHandler.WriteToken(jwt);
    }
}
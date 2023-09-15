using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class JwtBearerAuthenticationConfiguration
{
    public static IFunctionsHostBuilder AddJwtBearerAuthentication(this IFunctionsHostBuilder builder,
        IConfiguration configuration)
    {
        var issuer = configuration.GetSection("Jwt:Issuer").Value;
        var audience = configuration.GetSection("Jwt:Audience").Value;
        var secretKey = configuration.GetSection("Jwt:SecretKey").Value;
        
        builder.Services.AddSingleton<JwtSecurityTokenHandler>();
        
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        builder
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = symmetricKey,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return builder;
    }
}
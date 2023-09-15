#nullable enable
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection.Constants;

namespace Services.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? TryGetUserId(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypeConstants.UserId)?.Value;
    }
    
    public static string? TryGetUserName(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypeConstants.UserName)?.Value;
    }
}
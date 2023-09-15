#nullable enable
using Services.Entities;

namespace Services.Requests;

public sealed class LoginRequest
{
    public string? Name { get; set; }

    public string? Password { get; set; }
}
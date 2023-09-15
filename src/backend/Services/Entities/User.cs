using System.Collections.Generic;

namespace Services.Entities;

public sealed class User
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Password { get; set; } = default!;

    public ICollection<Game> Games { get; set; } = default!;
}
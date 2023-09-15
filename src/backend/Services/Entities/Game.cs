using Services.Enums;

namespace Services.Entities;

public sealed class Game
{
    public int Id { get; set; }
    
    public int Target { get; set; }
    
    public int UserId { get; set; }

    public GameState State { get; set; } = GameState.Started;

    public User User { get; set; } = default!;
}
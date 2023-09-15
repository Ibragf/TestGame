using System;

namespace Services.CServices;

public interface IRandomizer
{
    int GetInteger(int? min = null, int? max = null);
}

public sealed class Randomizer : IRandomizer
{
    private readonly Random _random;
    
    public Randomizer(Random random)
    {
        _random = random;
    }
    
    public int GetInteger(int? min = null, int? max = null)
    {
        return _random.Next(min ?? int.MinValue, max ?? int.MaxValue);
    }
}
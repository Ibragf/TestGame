using System;
using Services.CServices;

namespace Microsoft.Extensions.DependencyInjection;

public static class RandomizerConfiguration
{
    public static IServiceCollection AddRandomizer(this IServiceCollection services)
    {
        return services
            .AddSingleton(Random.Shared)
            .AddSingleton<IRandomizer, Randomizer>();
    }
}
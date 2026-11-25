using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PokeApiNet;

namespace PokeStream.Game;

public static class GameExtensions
{
    /// <summary>Registers the game rules. The host must register <c>PokeStreamDbContext</c>.</summary>
    public static IServiceCollection AddPokeStreamGame(this IServiceCollection services)
    {
        services.TryAddSingleton(_ => new PokeApiClient()); // one instance: it caches responses
        services.TryAddSingleton<IGameEvents, NoGameEvents>();
        services.AddScoped<GameService>();
        return services;
    }
}

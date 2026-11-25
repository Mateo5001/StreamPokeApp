using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PokeStream.Bot;

public static class TwitchBotExtensions
{
    /// <summary>Hosts the Twitch bot when <c>Bot:Enabled</c> is true. The host must register <c>PokeStreamDbContext</c>.</summary>
    public static IServiceCollection AddTwitchBot(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("Bot:Enabled"))
            return services;

        services.Configure<TwitchBotSettings>(configuration.GetSection("Twitch"));
        services.Configure<TwitchBotSettings>(configuration.GetSection("Bot"));
        services.AddHttpClient();
        services.AddDataProtection();
        services.AddSingleton<TwitchBotApi>();
        services.AddHostedService<TwitchBotService>();
        return services;
    }
}

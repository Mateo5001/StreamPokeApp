using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeStream.Game;

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
        services.AddPokeStreamGame();
        services.AddSingleton<TwitchBotApi>();
        services.AddSingleton<ChatSender>();
        services.AddSingleton<ChannelGame>();
        services.AddHostedService(provider => provider.GetRequiredService<ChannelGame>());
        services.AddHostedService<TwitchBotService>();
        return services;
    }
}

namespace PokeStream.Core.Settings;

/// <summary>Twitch application registered at dev.twitch.tv.</summary>
public class TwitchSettings
{
    public const string SectionName = "Twitch";

    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>Must match the one registered in Twitch exactly; it is the creator page of the web app.</summary>
    public string RedirectUri { get; set; } = string.Empty;
}

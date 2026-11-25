namespace PokeStream.Bot;

/// <summary>Bound from the <c>Twitch</c> (application credentials) and <c>Bot</c> configuration sections.</summary>
public class TwitchBotSettings
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>Off by default. Turn it off wherever the bot must not run, so two instances never answer twice.</summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Refresh token of the bot account (<c>twitch token -u -s 'user:read:chat user:write:chat'</c>).
    /// Only seeds the database; the rotated token is kept there. Changing this value re-seeds it.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}

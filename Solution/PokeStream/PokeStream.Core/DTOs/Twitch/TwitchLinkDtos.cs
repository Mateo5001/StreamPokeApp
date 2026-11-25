using System.ComponentModel.DataAnnotations;

namespace PokeStream.Core.DTOs.Twitch;

/// <summary>Twitch authorization page the browser must visit.</summary>
public record TwitchConnectResponse(string Url);

/// <summary>Values Twitch appended to the redirect URI.</summary>
public record TwitchLinkRequest([Required] string Code, [Required] string State);

namespace PokeStream.Core.DTOs.User;

/// <summary>
/// Info básica del usuario en responses
/// </summary>
public class UserInfoDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HasPassword { get; set; }
    public bool HasTwitchLinked { get; set; }
    public bool HasYouTubeLinked { get; set; }
    public bool HasKickLinked { get; set; }
    public bool HasDiscordLinked { get; set; }
}
using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Auth;

/// <summary>Register screen: field names and validation messages of RegisterRequest. Strongly-typed access to <c>RegisterResource.resx</c> (add a key there AND here).</summary>
public static class RegisterResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Auth.RegisterResource", typeof(RegisterResource).Assembly);
    public static string Username => Manager.GetString(nameof(Username), CultureInfo.CurrentUICulture) ?? nameof(Username);
    public static string Email => Manager.GetString(nameof(Email), CultureInfo.CurrentUICulture) ?? nameof(Email);
    public static string Password => Manager.GetString(nameof(Password), CultureInfo.CurrentUICulture) ?? nameof(Password);
    public static string DisplayName => Manager.GetString(nameof(DisplayName), CultureInfo.CurrentUICulture) ?? nameof(DisplayName);
    public static string UsernameRequired => Manager.GetString(nameof(UsernameRequired), CultureInfo.CurrentUICulture) ?? nameof(UsernameRequired);
    public static string UsernameMinLength => Manager.GetString(nameof(UsernameMinLength), CultureInfo.CurrentUICulture) ?? nameof(UsernameMinLength);
    public static string UsernameMaxLength => Manager.GetString(nameof(UsernameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(UsernameMaxLength);
    public static string EmailRequired => Manager.GetString(nameof(EmailRequired), CultureInfo.CurrentUICulture) ?? nameof(EmailRequired);
    public static string EmailInvalid => Manager.GetString(nameof(EmailInvalid), CultureInfo.CurrentUICulture) ?? nameof(EmailInvalid);
    public static string EmailMaxLength => Manager.GetString(nameof(EmailMaxLength), CultureInfo.CurrentUICulture) ?? nameof(EmailMaxLength);
    public static string PasswordRequired => Manager.GetString(nameof(PasswordRequired), CultureInfo.CurrentUICulture) ?? nameof(PasswordRequired);
    public static string PasswordMinLength => Manager.GetString(nameof(PasswordMinLength), CultureInfo.CurrentUICulture) ?? nameof(PasswordMinLength);
    public static string PasswordMaxLength => Manager.GetString(nameof(PasswordMaxLength), CultureInfo.CurrentUICulture) ?? nameof(PasswordMaxLength);
    public static string DisplayNameMaxLength => Manager.GetString(nameof(DisplayNameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(DisplayNameMaxLength);
    public static string TwitchUserIdMaxLength => Manager.GetString(nameof(TwitchUserIdMaxLength), CultureInfo.CurrentUICulture) ?? nameof(TwitchUserIdMaxLength);
    public static string TwitchUsernameMaxLength => Manager.GetString(nameof(TwitchUsernameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(TwitchUsernameMaxLength);
    public static string YouTubeChannelIdMaxLength => Manager.GetString(nameof(YouTubeChannelIdMaxLength), CultureInfo.CurrentUICulture) ?? nameof(YouTubeChannelIdMaxLength);
    public static string YouTubeUsernameMaxLength => Manager.GetString(nameof(YouTubeUsernameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(YouTubeUsernameMaxLength);
    public static string KickUserIdMaxLength => Manager.GetString(nameof(KickUserIdMaxLength), CultureInfo.CurrentUICulture) ?? nameof(KickUserIdMaxLength);
    public static string KickUsernameMaxLength => Manager.GetString(nameof(KickUsernameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(KickUsernameMaxLength);
    public static string DiscordUserIdMaxLength => Manager.GetString(nameof(DiscordUserIdMaxLength), CultureInfo.CurrentUICulture) ?? nameof(DiscordUserIdMaxLength);
    public static string DiscordUsernameMaxLength => Manager.GetString(nameof(DiscordUsernameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(DiscordUsernameMaxLength);
}

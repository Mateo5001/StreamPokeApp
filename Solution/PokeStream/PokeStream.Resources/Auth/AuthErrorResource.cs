using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Auth;

/// <summary>Messages returned by the auth API (AuthController) and AuthService. Strongly-typed access to <c>AuthErrorResource.resx</c> (add a key there AND here).</summary>
public static class AuthErrorResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Auth.AuthErrorResource", typeof(AuthErrorResource).Assembly);
    public static string RegisterInvalidData => Manager.GetString(nameof(RegisterInvalidData), CultureInfo.CurrentUICulture) ?? nameof(RegisterInvalidData);
    public static string LoginInvalidData => Manager.GetString(nameof(LoginInvalidData), CultureInfo.CurrentUICulture) ?? nameof(LoginInvalidData);
    public static string RefreshTokenRequired => Manager.GetString(nameof(RefreshTokenRequired), CultureInfo.CurrentUICulture) ?? nameof(RefreshTokenRequired);
    public static string SessionNotFound => Manager.GetString(nameof(SessionNotFound), CultureInfo.CurrentUICulture) ?? nameof(SessionNotFound);
    public static string LogoutSuccess => Manager.GetString(nameof(LogoutSuccess), CultureInfo.CurrentUICulture) ?? nameof(LogoutSuccess);
    public static string InvalidToken => Manager.GetString(nameof(InvalidToken), CultureInfo.CurrentUICulture) ?? nameof(InvalidToken);
    public static string UserNotFound => Manager.GetString(nameof(UserNotFound), CultureInfo.CurrentUICulture) ?? nameof(UserNotFound);
    public static string UsernameInUse => Manager.GetString(nameof(UsernameInUse), CultureInfo.CurrentUICulture) ?? nameof(UsernameInUse);
    public static string EmailInUse => Manager.GetString(nameof(EmailInUse), CultureInfo.CurrentUICulture) ?? nameof(EmailInUse);
    public static string InvalidCredentials => Manager.GetString(nameof(InvalidCredentials), CultureInfo.CurrentUICulture) ?? nameof(InvalidCredentials);
    public static string AccountDisabled => Manager.GetString(nameof(AccountDisabled), CultureInfo.CurrentUICulture) ?? nameof(AccountDisabled);
    public static string RefreshTokenInvalidOrExpired => Manager.GetString(nameof(RefreshTokenInvalidOrExpired), CultureInfo.CurrentUICulture) ?? nameof(RefreshTokenInvalidOrExpired);
}

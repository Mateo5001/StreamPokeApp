using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Auth;

/// <summary>Register screen: field names and validation messages of RegisterRequest. Strongly-typed access to <c>RegisterResource.resx</c> (add a key there AND here).</summary>
public static class RegisterResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Auth.RegisterResource", typeof(RegisterResource).Assembly);
    public static string Title => Manager.GetString(nameof(Title), CultureInfo.CurrentUICulture) ?? nameof(Title);
    public static string Subtitle => Manager.GetString(nameof(Subtitle), CultureInfo.CurrentUICulture) ?? nameof(Subtitle);
    public static string ConfirmPassword => Manager.GetString(nameof(ConfirmPassword), CultureInfo.CurrentUICulture) ?? nameof(ConfirmPassword);
    public static string PasswordsDoNotMatch => Manager.GetString(nameof(PasswordsDoNotMatch), CultureInfo.CurrentUICulture) ?? nameof(PasswordsDoNotMatch);
    public static string ShowHidePassword => Manager.GetString(nameof(ShowHidePassword), CultureInfo.CurrentUICulture) ?? nameof(ShowHidePassword);
    public static string SubmitButton => Manager.GetString(nameof(SubmitButton), CultureInfo.CurrentUICulture) ?? nameof(SubmitButton);
    public static string RegisterFailed => Manager.GetString(nameof(RegisterFailed), CultureInfo.CurrentUICulture) ?? nameof(RegisterFailed);
    public static string HaveAccount => Manager.GetString(nameof(HaveAccount), CultureInfo.CurrentUICulture) ?? nameof(HaveAccount);
    public static string SignIn => Manager.GetString(nameof(SignIn), CultureInfo.CurrentUICulture) ?? nameof(SignIn);
    public static string Username =>Manager.GetString(nameof(Username), CultureInfo.CurrentUICulture) ?? nameof(Username);
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
    public static string DiscordUserIdMaxLength => Manager.GetString(nameof(DiscordUserIdMaxLength), CultureInfo.CurrentUICulture) ?? nameof(DiscordUserIdMaxLength);
    public static string DiscordUsernameMaxLength => Manager.GetString(nameof(DiscordUsernameMaxLength), CultureInfo.CurrentUICulture) ?? nameof(DiscordUsernameMaxLength);
}

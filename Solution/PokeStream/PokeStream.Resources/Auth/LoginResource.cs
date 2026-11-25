using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Auth;

/// <summary>Login screen: texts and validation messages of LoginRequest. Strongly-typed access to <c>LoginResource.resx</c> (add a key there AND here).</summary>
public static class LoginResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Auth.LoginResource", typeof(LoginResource).Assembly);
    public static string Title => Manager.GetString(nameof(Title), CultureInfo.CurrentUICulture) ?? nameof(Title);
    public static string UsernameOrEmail => Manager.GetString(nameof(UsernameOrEmail), CultureInfo.CurrentUICulture) ?? nameof(UsernameOrEmail);
    public static string Password => Manager.GetString(nameof(Password), CultureInfo.CurrentUICulture) ?? nameof(Password);
    public static string ShowHidePassword => Manager.GetString(nameof(ShowHidePassword), CultureInfo.CurrentUICulture) ?? nameof(ShowHidePassword);
    public static string RememberMe => Manager.GetString(nameof(RememberMe), CultureInfo.CurrentUICulture) ?? nameof(RememberMe);
    public static string ForgotPassword => Manager.GetString(nameof(ForgotPassword), CultureInfo.CurrentUICulture) ?? nameof(ForgotPassword);
    public static string SubmitButton => Manager.GetString(nameof(SubmitButton), CultureInfo.CurrentUICulture) ?? nameof(SubmitButton);
    public static string OrContinueWith => Manager.GetString(nameof(OrContinueWith), CultureInfo.CurrentUICulture) ?? nameof(OrContinueWith);
    public static string NoAccount => Manager.GetString(nameof(NoAccount), CultureInfo.CurrentUICulture) ?? nameof(NoAccount);
    public static string CreateAccount => Manager.GetString(nameof(CreateAccount), CultureInfo.CurrentUICulture) ?? nameof(CreateAccount);
    public static string InvalidCredentials => Manager.GetString(nameof(InvalidCredentials), CultureInfo.CurrentUICulture) ?? nameof(InvalidCredentials);
    public static string UsernameOrEmailRequired => Manager.GetString(nameof(UsernameOrEmailRequired), CultureInfo.CurrentUICulture) ?? nameof(UsernameOrEmailRequired);
    public static string UsernameOrEmailMaxLength => Manager.GetString(nameof(UsernameOrEmailMaxLength), CultureInfo.CurrentUICulture) ?? nameof(UsernameOrEmailMaxLength);
    public static string PasswordRequired => Manager.GetString(nameof(PasswordRequired), CultureInfo.CurrentUICulture) ?? nameof(PasswordRequired);
    public static string PasswordMaxLength => Manager.GetString(nameof(PasswordMaxLength), CultureInfo.CurrentUICulture) ?? nameof(PasswordMaxLength);
    public static string DeviceInfoMaxLength => Manager.GetString(nameof(DeviceInfoMaxLength), CultureInfo.CurrentUICulture) ?? nameof(DeviceInfoMaxLength);
    public static string UserAgentMaxLength => Manager.GetString(nameof(UserAgentMaxLength), CultureInfo.CurrentUICulture) ?? nameof(UserAgentMaxLength);
    public static string IpAddressMaxLength => Manager.GetString(nameof(IpAddressMaxLength), CultureInfo.CurrentUICulture) ?? nameof(IpAddressMaxLength);
}

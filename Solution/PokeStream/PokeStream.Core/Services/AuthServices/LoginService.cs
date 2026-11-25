using PokeStream.Core.DTOs.Auth;
using PokeStream.Core.Services.Cookies;
using PokeStream.Resources.Auth;
using PokeStream.Resources.Common;

namespace PokeStream.Core.Services.AuthServices;

/// <summary>Sign-in flow shared by the login page and the embedded login.</summary>
public class LoginService
{
    private const int RememberedUserDays = 30;

    private readonly AuthStateService _auth;
    private readonly ICookieService _cookies;

    public LoginService(AuthStateService auth, ICookieService cookies)
    {
        _auth = auth;
        _cookies = cookies;
    }

    /// <summary>Signs in and stores or forgets the user name. Returns null on success, otherwise the message to show.</summary>
    public async Task<string?> LoginAsync(LoginRequest request, bool remember)
    {
        try
        {
            if (await _auth.LoginAsync(request) is null)
                return LoginResource.InvalidCredentials;

            if (remember)
                await _cookies.SetAsync(CookieKeys.RememberedUser, request.UsernameOrEmail, RememberedUserDays);
            else
                await _cookies.RemoveAsync(CookieKeys.RememberedUser);

            request.Password = string.Empty;
            return null;
        }
        catch (HttpRequestException)
        {
            return CommonResource.ServerUnreachable;
        }
    }
}

namespace PokeStream.Core.Services.Cookies;

/// <summary>Names of the cookies the app uses, plus the endpoint that issues the HttpOnly one.</summary>
public static class CookieKeys
{
    /// <summary>Session tokens (JSON). HttpOnly: use <see cref="ICookieService.SetSecureAsync"/> / <see cref="ICookieService.GetSecureAsync"/>.</summary>
    public const string Session = "ps_session";

    /// <summary>Selected theme ("dark" / "light"). Readable by JS: use <see cref="ICookieService.SetAsync"/>.</summary>
    public const string Theme = "ps_theme";

    /// <summary>Username or email remembered by the "Recordarme" checkbox. Readable by JS.</summary>
    public const string RememberedUser = "ps_user";

    /// <summary>Server endpoint (mapped in UI.Web) that sets/removes the HttpOnly <see cref="Session"/> cookie.</summary>
    public const string SessionEndpoint = "/cookies/session";

    /// <summary>Max accepted cookie value length (browsers cap a cookie at ~4 KB).</summary>
    public const int MaxValueLength = 4000;
}

/// <summary>Body of POST <see cref="CookieKeys.SessionEndpoint"/>. <paramref name="Days"/> null = session cookie.</summary>
public record SessionCookieRequest(string Value, int? Days);

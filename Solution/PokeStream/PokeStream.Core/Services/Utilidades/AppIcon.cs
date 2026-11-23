namespace PokeStream.Core.Services.Utilidades;

/// <summary>
/// Single source of truth for every icon used in PokeStream.UI.Web.
/// Each icon is a <c>public const string</c> holding SVG inner markup (the same format as MudBlazor's
/// <c>Icons.Material.*</c> constants), so it can be passed straight to any MudBlazor <c>Icon</c> parameter.
///
/// Usage in Razor:   <c>&lt;MudIcon Icon="@AppIcon.Pokeball" /&gt;</c>
///                   <c>&lt;MudIconButton Icon="@AppIcon.Menu" /&gt;</c>
///
/// To add an icon: add a new constant below (PascalCase name). Never use <c>Icons.Material.*</c> directly in components.
/// </summary>
public static class AppIcon
{
    /// <summary>
    /// Paths (relative to wwwroot) of the "PokéStream" wordmark images, letters converted to outlines.
    /// Usage in Razor: <c>&lt;img src="@AppIcon.Images.WordmarkOrange" alt="PokéStream" /&gt;</c>
    /// </summary>
    public static class Images
    {
        /// <summary>"Poké" light orange + "Stream" white, for blue backgrounds.</summary>
        public const string WordmarkOnBlue = "logo/wordmark-on-blue.svg";

        /// <summary>"Poké" white + "Stream" blue, for orange backgrounds.</summary>
        public const string WordmarkOnOrange = "logo/wordmark-on-orange.svg";

        /// <summary>Account avatar for light mode: orange disc, blue ring, navy trainer silhouette.</summary>
        public const string AccountLight = "logo/account-light.svg";

        /// <summary>Account avatar for dark mode: navy disc, orange ring and silhouette.</summary>
        public const string AccountDark = "logo/account-dark.svg";
    }

    /// <summary>Poké Ball + play logo for dark mode: orange ring/play, dark-blue body. Drawn on a 200x200 grid, scaled to the 24x24 MudBlazor viewBox. Mirrors <c>wwwroot/logo/logo-blue.svg</c> (with the drop shadow).</summary>
    public const string LogoBlue = "<g transform=\"scale(0.12)\"><defs><mask id=\"play-blue\"><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#fff\" stroke=\"#fff\" stroke-width=\"10\" stroke-linejoin=\"round\"/></mask><filter id=\"shadow-blue\" x=\"-20%\" y=\"-20%\" width=\"140%\" height=\"150%\"><feDropShadow dx=\"0\" dy=\"3\" stdDeviation=\"2.5\" flood-color=\"#0A1432\" flood-opacity=\"0.35\"/></filter></defs><g filter=\"url(#shadow-blue)\"><circle cx=\"100\" cy=\"100\" r=\"86\" fill=\"#FF8A2A\"/><circle cx=\"100\" cy=\"100\" r=\"75\" fill=\"#101842\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#FF8A2A\"/><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#FF8A2A\" stroke=\"#FF8A2A\" stroke-width=\"10\" stroke-linejoin=\"round\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#101842\" mask=\"url(#play-blue)\"/><circle cx=\"100\" cy=\"100\" r=\"20\" fill=\"#FF8A2A\" stroke=\"#101842\" stroke-width=\"11\"/></g></g>";

    /// <summary>Poké Ball + play logo for light mode: blue ring/play, orange body. Drawn on a 200x200 grid, scaled to the 24x24 MudBlazor viewBox. Mirrors <c>wwwroot/logo/logo-orange.svg</c> (with the drop shadow).</summary>
    public const string LogoOrange = "<g transform=\"scale(0.12)\"><defs><mask id=\"play-orange\"><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#fff\" stroke=\"#fff\" stroke-width=\"10\" stroke-linejoin=\"round\"/></mask><filter id=\"shadow-orange\" x=\"-20%\" y=\"-20%\" width=\"140%\" height=\"150%\"><feDropShadow dx=\"0\" dy=\"3\" stdDeviation=\"2.5\" flood-color=\"#0A1432\" flood-opacity=\"0.35\"/></filter></defs><g filter=\"url(#shadow-orange)\"><circle cx=\"100\" cy=\"100\" r=\"86\" fill=\"#2E6BFF\"/><circle cx=\"100\" cy=\"100\" r=\"75\" fill=\"#FF7A1A\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#2E6BFF\"/><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#2E6BFF\" stroke=\"#2E6BFF\" stroke-width=\"10\" stroke-linejoin=\"round\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#FF7A1A\" mask=\"url(#play-orange)\"/><circle cx=\"100\" cy=\"100\" r=\"20\" fill=\"#2E6BFF\" stroke=\"#FF7A1A\" stroke-width=\"11\"/></g></g>";

    /// <summary>Account / default avatar: user silhouette with a cap with a curved visor and a stylized "P" on the front. Single color (currentColor), 24x24 grid.</summary>
    public const string Account = "<path fill-rule=\"evenodd\" d=\"M7 8.2a5 5 0 0 1 10 0z M4.5 9.6C4.5 8.9 8 8.7 12 8.7s7.5.2 7.5.9c0 1.6-3.6 2.4-7.5 2.4S4.5 11.2 4.5 9.6z M8.2 12.8h7.6a3.8 3.8 0 0 1-7.6 0z M4 21v-.9c0-2 3.6-2.9 8-2.9s8 .9 8 2.9V21z M10.5 4h1.8a1.3 1.3 0 0 1 0 2.6h-.8V7.8h-1z M11.5 4.9h.9a.4.4 0 0 1 0 .8h-.9z\"/>";

    /// <summary>Account avatar for light mode (colored, 24x24): orange disc, blue ring, navy silhouette. Use as default profile picture. Mirrors <c>wwwroot/logo/account-light.svg</c>.</summary>
    public const string AccountLight = "<circle cx=\"12\" cy=\"12\" r=\"11.5\" fill=\"#FF7A1A\" stroke=\"#2E6BFF\" stroke-width=\"1\"/><path transform=\"translate(3 3.2) scale(0.75)\" fill=\"#101842\" fill-rule=\"evenodd\" d=\"M7 8.2a5 5 0 0 1 10 0z M4.5 9.6C4.5 8.9 8 8.7 12 8.7s7.5.2 7.5.9c0 1.6-3.6 2.4-7.5 2.4S4.5 11.2 4.5 9.6z M8.2 12.8h7.6a3.8 3.8 0 0 1-7.6 0z M4 21v-.9c0-2 3.6-2.9 8-2.9s8 .9 8 2.9V21z M10.5 4h1.8a1.3 1.3 0 0 1 0 2.6h-.8V7.8h-1z M11.5 4.9h.9a.4.4 0 0 1 0 .8h-.9z\"/>";

    /// <summary>Account avatar for dark mode (colored, 24x24): navy disc, orange ring and silhouette. Use as default profile picture. Mirrors <c>wwwroot/logo/account-dark.svg</c>.</summary>
    public const string AccountDark = "<circle cx=\"12\" cy=\"12\" r=\"11.5\" fill=\"#101842\" stroke=\"#FF8A2A\" stroke-width=\"1\"/><path transform=\"translate(3 3.2) scale(0.75)\" fill=\"#FF8A2A\" fill-rule=\"evenodd\" d=\"M7 8.2a5 5 0 0 1 10 0z M4.5 9.6C4.5 8.9 8 8.7 12 8.7s7.5.2 7.5.9c0 1.6-3.6 2.4-7.5 2.4S4.5 11.2 4.5 9.6z M8.2 12.8h7.6a3.8 3.8 0 0 1-7.6 0z M4 21v-.9c0-2 3.6-2.9 8-2.9s8 .9 8 2.9V21z M10.5 4h1.8a1.3 1.3 0 0 1 0 2.6h-.8V7.8h-1z M11.5 4.9h.9a.4.4 0 0 1 0 .8h-.9z\"/>";

    /// <summary>Twitch glitch logo (single color, 24x24). Brand color: #9146FF.</summary>
    public const string Twitch = "<path d=\"M11.571 4.714h1.715v5.143H11.57zm4.715 0H18v5.143h-1.714zM6 0L1.714 4.286v15.428h5.143V24l4.286-4.286h3.428L22.286 12V0zm14.571 11.143l-3.428 3.428h-3.429l-3 3v-3H6.857V1.714h13.714Z\"/>";

    /// <summary>Kick pixel "K" logo (single color, 24x24). Brand color: #53FC18.</summary>
    public const string Kick = "<path d=\"M1.333 0h8v5.333H12V2.667h2.667V0h8v8H20v2.667h-2.667v2.666H20V16h2.667v8h-8v-2.667H12v-2.666H9.333V24h-8Z\"/>";

    /// <summary>YouTube play-button logo (single color, 24x24). Brand color: #FF0000.</summary>
    public const string YouTube = "<path d=\"M23.498 6.186a3.016 3.016 0 0 0-2.122-2.136C19.505 3.545 12 3.545 12 3.545s-7.505 0-9.377.505A3.017 3.017 0 0 0 .502 6.186C0 8.07 0 12 0 12s0 3.93.502 5.814a3.016 3.016 0 0 0 2.122 2.136c1.871.505 9.376.505 9.376.505s7.505 0 9.377-.505a3.015 3.015 0 0 0 2.122-2.136C24 15.93 24 12 24 12s0-3.93-.502-5.814zM9.545 15.568V8.432L15.818 12l-6.273 3.568z\"/>";

    /// <summary>Eye (show password).</summary>
    public const string Visibility = "<path d=\"M12 4.5C7 4.5 2.73 7.61 1 12c1.73 4.39 6 7.5 11 7.5s9.27-3.11 11-7.5c-1.73-4.39-6-7.5-11-7.5zM12 17c-2.76 0-5-2.24-5-5s2.24-5 5-5 5 2.24 5 5-2.24 5-5 5zm0-8c-1.66 0-3 1.34-3 3s1.34 3 3 3 3-1.34 3-3-1.34-3-3-3z\"/>";

    /// <summary>Crossed-out eye (hide password).</summary>
    public const string VisibilityOff = "<path d=\"M12 7c2.76 0 5 2.24 5 5 0 .65-.13 1.26-.36 1.83l2.92 2.92c1.51-1.26 2.7-2.89 3.43-4.75-1.73-4.39-6-7.5-11-7.5-1.4 0-2.74.25-3.98.7l2.16 2.16C10.74 7.13 11.35 7 12 7zM2 4.27l2.28 2.28.46.46C3.08 8.3 1.78 10.02 1 12c1.73 4.39 6 7.5 11 7.5 1.55 0 3.03-.3 4.38-.84l.42.42L19.73 22 21 20.73 3.27 3 2 4.27zM7.53 9.8l1.55 1.55c-.05.21-.08.43-.08.65 0 1.66 1.34 3 3 3 .22 0 .44-.03.65-.08l1.55 1.55c-.67.33-1.41.53-2.2.53-2.76 0-5-2.24-5-5 0-.79.2-1.53.53-2.2zm4.31-.78l3.15 3.15.02-.16c0-1.66-1.34-3-3-3l-.17.01z\"/>";

    public const string DarkMode ="<path d=\"M12 3c-4.97 0-9 4.03-9 9s4.03 9 9 9 9-4.03 9-9c0-.46-.04-.92-.1-1.36-.98 1.37-2.58 2.26-4.4 2.26-2.98 0-5.4-2.42-5.4-5.4 0-1.81.89-3.42 2.26-4.4-.44-.06-.9-.1-1.36-.1z\"/>";

    public const string LightMode = "<path d=\"M12 7c-2.76 0-5 2.24-5 5s2.24 5 5 5 5-2.24 5-5-2.24-5-5-5zM2 13h2c.55 0 1-.45 1-1s-.45-1-1-1H2c-.55 0-1 .45-1 1s.45 1 1 1zm18 0h2c.55 0 1-.45 1-1s-.45-1-1-1h-2c-.55 0-1 .45-1 1s.45 1 1 1zM11 2v2c0 .55.45 1 1 1s1-.45 1-1V2c0-.55-.45-1-1-1s-1 .45-1 1zm0 18v2c0 .55.45 1 1 1s1-.45 1-1v-2c0-.55-.45-1-1-1s-1 .45-1 1zM5.99 4.58a.996.996 0 00-1.41 0 .996.996 0 000 1.41l1.06 1.06c.39.39 1.03.39 1.41 0s.39-1.03 0-1.41L5.99 4.58zm12.37 12.37a.996.996 0 00-1.41 0 .996.996 0 000 1.41l1.06 1.06c.39.39 1.03.39 1.41 0a.996.996 0 000-1.41l-1.06-1.06zm1.06-10.96a.996.996 0 000-1.41.996.996 0 00-1.41 0l-1.06 1.06c-.39.39-.39 1.03 0 1.41s1.03.39 1.41 0l1.06-1.06zM7.05 18.36a.996.996 0 000-1.41.996.996 0 00-1.41 0l-1.06 1.06c-.39.39-.39 1.03 0 1.41s1.03.39 1.41 0l1.06-1.06z\"/>";
}

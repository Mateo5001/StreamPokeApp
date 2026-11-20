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
    }

    /// <summary>Poké Ball + play logo for dark mode: orange ring/play, dark-blue body. Drawn on a 200x200 grid, scaled to the 24x24 MudBlazor viewBox. Mirrors <c>wwwroot/logo/logo-blue.svg</c> (with the drop shadow).</summary>
    public const string LogoBlue = "<g transform=\"scale(0.12)\"><defs><mask id=\"play-blue\"><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#fff\" stroke=\"#fff\" stroke-width=\"10\" stroke-linejoin=\"round\"/></mask><filter id=\"shadow-blue\" x=\"-20%\" y=\"-20%\" width=\"140%\" height=\"150%\"><feDropShadow dx=\"0\" dy=\"3\" stdDeviation=\"2.5\" flood-color=\"#0A1432\" flood-opacity=\"0.35\"/></filter></defs><g filter=\"url(#shadow-blue)\"><circle cx=\"100\" cy=\"100\" r=\"86\" fill=\"#FF8A2A\"/><circle cx=\"100\" cy=\"100\" r=\"75\" fill=\"#101842\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#FF8A2A\"/><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#FF8A2A\" stroke=\"#FF8A2A\" stroke-width=\"10\" stroke-linejoin=\"round\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#101842\" mask=\"url(#play-blue)\"/><circle cx=\"100\" cy=\"100\" r=\"20\" fill=\"#FF8A2A\" stroke=\"#101842\" stroke-width=\"11\"/></g></g>";

    /// <summary>Poké Ball + play logo for light mode: blue ring/play, orange body. Drawn on a 200x200 grid, scaled to the 24x24 MudBlazor viewBox. Mirrors <c>wwwroot/logo/logo-orange.svg</c> (with the drop shadow).</summary>
    public const string LogoOrange = "<g transform=\"scale(0.12)\"><defs><mask id=\"play-orange\"><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#fff\" stroke=\"#fff\" stroke-width=\"10\" stroke-linejoin=\"round\"/></mask><filter id=\"shadow-orange\" x=\"-20%\" y=\"-20%\" width=\"140%\" height=\"150%\"><feDropShadow dx=\"0\" dy=\"3\" stdDeviation=\"2.5\" flood-color=\"#0A1432\" flood-opacity=\"0.35\"/></filter></defs><g filter=\"url(#shadow-orange)\"><circle cx=\"100\" cy=\"100\" r=\"86\" fill=\"#2E6BFF\"/><circle cx=\"100\" cy=\"100\" r=\"75\" fill=\"#FF7A1A\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#2E6BFF\"/><path d=\"M68 46 L68 154 L156 100 Z\" fill=\"#2E6BFF\" stroke=\"#2E6BFF\" stroke-width=\"10\" stroke-linejoin=\"round\"/><rect x=\"14\" y=\"94.5\" width=\"172\" height=\"11\" fill=\"#FF7A1A\" mask=\"url(#play-orange)\"/><circle cx=\"100\" cy=\"100\" r=\"20\" fill=\"#2E6BFF\" stroke=\"#FF7A1A\" stroke-width=\"11\"/></g></g>";

    public const string DarkMode = "<path d=\"M12 3c-4.97 0-9 4.03-9 9s4.03 9 9 9 9-4.03 9-9c0-.46-.04-.92-.1-1.36-.98 1.37-2.58 2.26-4.4 2.26-2.98 0-5.4-2.42-5.4-5.4 0-1.81.89-3.42 2.26-4.4-.44-.06-.9-.1-1.36-.1z\"/>";

    public const string LightMode = "<path d=\"M12 7c-2.76 0-5 2.24-5 5s2.24 5 5 5 5-2.24 5-5-2.24-5-5-5zM2 13h2c.55 0 1-.45 1-1s-.45-1-1-1H2c-.55 0-1 .45-1 1s.45 1 1 1zm18 0h2c.55 0 1-.45 1-1s-.45-1-1-1h-2c-.55 0-1 .45-1 1s.45 1 1 1zM11 2v2c0 .55.45 1 1 1s1-.45 1-1V2c0-.55-.45-1-1-1s-1 .45-1 1zm0 18v2c0 .55.45 1 1 1s1-.45 1-1v-2c0-.55-.45-1-1-1s-1 .45-1 1zM5.99 4.58a.996.996 0 00-1.41 0 .996.996 0 000 1.41l1.06 1.06c.39.39 1.03.39 1.41 0s.39-1.03 0-1.41L5.99 4.58zm12.37 12.37a.996.996 0 00-1.41 0 .996.996 0 000 1.41l1.06 1.06c.39.39 1.03.39 1.41 0a.996.996 0 000-1.41l-1.06-1.06zm1.06-10.96a.996.996 0 000-1.41.996.996 0 00-1.41 0l-1.06 1.06c-.39.39-.39 1.03 0 1.41s1.03.39 1.41 0l1.06-1.06zM7.05 18.36a.996.996 0 000-1.41.996.996 0 00-1.41 0l-1.06 1.06c-.39.39-.39 1.03 0 1.41s1.03.39 1.41 0l1.06-1.06z\"/>";
}

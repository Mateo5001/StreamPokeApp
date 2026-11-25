using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Creator;

/// <summary>Creator page. Strongly-typed access to <c>CreatorResource.resx</c> (add a key there AND here).</summary>
public static class CreatorResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Creator.CreatorResource", typeof(CreatorResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string Title => Manager.GetString(nameof(Title), CultureInfo.CurrentUICulture) ?? nameof(Title);
    public static string Subtitle => Manager.GetString(nameof(Subtitle), CultureInfo.CurrentUICulture) ?? nameof(Subtitle);
    public static string TwitchTitle => Manager.GetString(nameof(TwitchTitle), CultureInfo.CurrentUICulture) ?? nameof(TwitchTitle);
    public static string TwitchRequired => Manager.GetString(nameof(TwitchRequired), CultureInfo.CurrentUICulture) ?? nameof(TwitchRequired);
    public static string TwitchNotConnected => Manager.GetString(nameof(TwitchNotConnected), CultureInfo.CurrentUICulture) ?? nameof(TwitchNotConnected);
    public static string TwitchConnect => Manager.GetString(nameof(TwitchConnect), CultureInfo.CurrentUICulture) ?? nameof(TwitchConnect);
    public static string OverlayTitle => Manager.GetString(nameof(OverlayTitle), CultureInfo.CurrentUICulture) ?? nameof(OverlayTitle);
    public static string OverlayUrl => Manager.GetString(nameof(OverlayUrl), CultureInfo.CurrentUICulture) ?? nameof(OverlayUrl);
    public static string OverlayHint => Manager.GetString(nameof(OverlayHint), CultureInfo.CurrentUICulture) ?? nameof(OverlayHint);
    public static string OverlayLocked => Manager.GetString(nameof(OverlayLocked), CultureInfo.CurrentUICulture) ?? nameof(OverlayLocked);
    public static string Copy => Manager.GetString(nameof(Copy), CultureInfo.CurrentUICulture) ?? nameof(Copy);
    public static string OptionsTitle => Manager.GetString(nameof(OptionsTitle), CultureInfo.CurrentUICulture) ?? nameof(OptionsTitle);
    public static string ShowSpawns => Manager.GetString(nameof(ShowSpawns), CultureInfo.CurrentUICulture) ?? nameof(ShowSpawns);
    public static string PlaySounds => Manager.GetString(nameof(PlaySounds), CultureInfo.CurrentUICulture) ?? nameof(PlaySounds);
    public static string SpawnRate => Manager.GetString(nameof(SpawnRate), CultureInfo.CurrentUICulture) ?? nameof(SpawnRate);
    public static string ExampleNotice => Manager.GetString(nameof(ExampleNotice), CultureInfo.CurrentUICulture) ?? nameof(ExampleNotice);
    public static string TwitchConnectedAs => Manager.GetString(nameof(TwitchConnectedAs), CultureInfo.CurrentUICulture) ?? nameof(TwitchConnectedAs);
    public static string TwitchNotConfigured => Manager.GetString(nameof(TwitchNotConfigured), CultureInfo.CurrentUICulture) ?? nameof(TwitchNotConfigured);
    public static string TwitchLinkExpired => Manager.GetString(nameof(TwitchLinkExpired), CultureInfo.CurrentUICulture) ?? nameof(TwitchLinkExpired);
    public static string TwitchFailed => Manager.GetString(nameof(TwitchFailed), CultureInfo.CurrentUICulture) ?? nameof(TwitchFailed);
    public static string TwitchTaken => Manager.GetString(nameof(TwitchTaken), CultureInfo.CurrentUICulture) ?? nameof(TwitchTaken);
    public static string TwitchCancelled => Manager.GetString(nameof(TwitchCancelled), CultureInfo.CurrentUICulture) ?? nameof(TwitchCancelled);
    public static string OverlayRegenerate => Manager.GetString(nameof(OverlayRegenerate), CultureInfo.CurrentUICulture) ?? nameof(OverlayRegenerate);
    public static string Copied => Manager.GetString(nameof(Copied), CultureInfo.CurrentUICulture) ?? nameof(Copied);
    public static string TwitchBackToAccount => Manager.GetString(nameof(TwitchBackToAccount), CultureInfo.CurrentUICulture) ?? nameof(TwitchBackToAccount);
}

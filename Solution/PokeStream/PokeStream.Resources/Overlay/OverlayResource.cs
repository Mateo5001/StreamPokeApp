using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Overlay;

/// <summary>Stream overlay. Strongly-typed access to <c>OverlayResource.resx</c> (add a key there AND here).</summary>
public static class OverlayResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Overlay.OverlayResource", typeof(OverlayResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string Level => Manager.GetString(nameof(Level), CultureInfo.CurrentUICulture) ?? nameof(Level);
    public static string CaptureHint => Manager.GetString(nameof(CaptureHint), CultureInfo.CurrentUICulture) ?? nameof(CaptureHint);
    public static string Shiny => Manager.GetString(nameof(Shiny), CultureInfo.CurrentUICulture) ?? nameof(Shiny);
    public static string CapturedBy => Manager.GetString(nameof(CapturedBy), CultureInfo.CurrentUICulture) ?? nameof(CapturedBy);
    public static string Fled => Manager.GetString(nameof(Fled), CultureInfo.CurrentUICulture) ?? nameof(Fled);
    public static string InvalidToken => Manager.GetString(nameof(InvalidToken), CultureInfo.CurrentUICulture) ?? nameof(InvalidToken);
}

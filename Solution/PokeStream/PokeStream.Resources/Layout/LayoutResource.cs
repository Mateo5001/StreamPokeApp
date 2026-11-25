using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Layout;

/// <summary>Main layout: app bar, account menu and global error bar. Strongly-typed access to <c>LayoutResource.resx</c> (add a key there AND here).</summary>
public static class LayoutResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Layout.LayoutResource", typeof(LayoutResource).Assembly);
    public static string Menu => Manager.GetString(nameof(Menu), CultureInfo.CurrentUICulture) ?? nameof(Menu);
    public static string ToggleTheme => Manager.GetString(nameof(ToggleTheme), CultureInfo.CurrentUICulture) ?? nameof(ToggleTheme);
    public static string MyAccount => Manager.GetString(nameof(MyAccount), CultureInfo.CurrentUICulture) ?? nameof(MyAccount);
    public static string SignOut => Manager.GetString(nameof(SignOut), CultureInfo.CurrentUICulture) ?? nameof(SignOut);
    public static string UnhandledError => Manager.GetString(nameof(UnhandledError), CultureInfo.CurrentUICulture) ?? nameof(UnhandledError);
    public static string Reload => Manager.GetString(nameof(Reload), CultureInfo.CurrentUICulture) ?? nameof(Reload);
}

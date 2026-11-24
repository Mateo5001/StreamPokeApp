using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Layout;

/// <summary>Side navigation menu. Strongly-typed access to <c>NavMenuResource.resx</c> (add a key there AND here).</summary>
public static class NavMenuResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Layout.NavMenuResource", typeof(NavMenuResource).Assembly);
    public static string Home => Manager.GetString(nameof(Home), CultureInfo.CurrentUICulture) ?? nameof(Home);
    public static string Account => Manager.GetString(nameof(Account), CultureInfo.CurrentUICulture) ?? nameof(Account);
    public static string Pokedex => Manager.GetString(nameof(Pokedex), CultureInfo.CurrentUICulture) ?? nameof(Pokedex);
    public static string CreatorPanel => Manager.GetString(nameof(CreatorPanel), CultureInfo.CurrentUICulture) ?? nameof(CreatorPanel);
}

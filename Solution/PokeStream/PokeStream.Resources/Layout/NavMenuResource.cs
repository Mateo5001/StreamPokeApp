using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Layout;

/// <summary>Side navigation menu. Strongly-typed access to <c>NavMenuResource.resx</c> (add a key there AND here).</summary>
public static class NavMenuResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Layout.NavMenuResource", typeof(NavMenuResource).Assembly);
    public static string Home => Manager.GetString(nameof(Home), CultureInfo.CurrentUICulture) ?? nameof(Home);
}

using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Home;

/// <summary>Home page. Strongly-typed access to <c>HomeResource.resx</c> (add a key there AND here).</summary>
public static class HomeResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Home.HomeResource", typeof(HomeResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string Welcome => Manager.GetString(nameof(Welcome), CultureInfo.CurrentUICulture) ?? nameof(Welcome);
}

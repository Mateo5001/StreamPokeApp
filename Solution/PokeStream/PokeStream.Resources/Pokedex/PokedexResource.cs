using System.Globalization;
using System.Resources;

namespace PokeStream.Resources.Pokedex;

/// <summary>Pokedex page. Strongly-typed access to <c>PokedexResource.resx</c> (add a key there AND here).</summary>
public static class PokedexResource
{
    private static readonly ResourceManager Manager = new("PokeStream.Resources.Pokedex.PokedexResource", typeof(PokedexResource).Assembly);
    public static string PageTitle => Manager.GetString(nameof(PageTitle), CultureInfo.CurrentUICulture) ?? nameof(PageTitle);
    public static string Title => Manager.GetString(nameof(Title), CultureInfo.CurrentUICulture) ?? nameof(Title);
    public static string Overall => Manager.GetString(nameof(Overall), CultureInfo.CurrentUICulture) ?? nameof(Overall);
    public static string Caught => Manager.GetString(nameof(Caught), CultureInfo.CurrentUICulture) ?? nameof(Caught);
    public static string ExampleNotice => Manager.GetString(nameof(ExampleNotice), CultureInfo.CurrentUICulture) ?? nameof(ExampleNotice);
    public static string Gen1 => Manager.GetString(nameof(Gen1), CultureInfo.CurrentUICulture) ?? nameof(Gen1);
    public static string Gen2 => Manager.GetString(nameof(Gen2), CultureInfo.CurrentUICulture) ?? nameof(Gen2);
    public static string Gen3 => Manager.GetString(nameof(Gen3), CultureInfo.CurrentUICulture) ?? nameof(Gen3);
    public static string Gen4 => Manager.GetString(nameof(Gen4), CultureInfo.CurrentUICulture) ?? nameof(Gen4);
    public static string Gen5 => Manager.GetString(nameof(Gen5), CultureInfo.CurrentUICulture) ?? nameof(Gen5);
    public static string Gen6 => Manager.GetString(nameof(Gen6), CultureInfo.CurrentUICulture) ?? nameof(Gen6);
    public static string Gen7 => Manager.GetString(nameof(Gen7), CultureInfo.CurrentUICulture) ?? nameof(Gen7);
    public static string Gen8 => Manager.GetString(nameof(Gen8), CultureInfo.CurrentUICulture) ?? nameof(Gen8);
    public static string Gen9 => Manager.GetString(nameof(Gen9), CultureInfo.CurrentUICulture) ?? nameof(Gen9);
}

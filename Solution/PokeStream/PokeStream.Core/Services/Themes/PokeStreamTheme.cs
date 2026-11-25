using MudBlazor;

namespace PokeStream.Core.Services.Themes;

/// <summary>
/// Custom MudBlazor theme for PokéStream. Every top-level MudTheme parameter is instantiated here,
/// so this is the single place to tune the look of the web app.
/// Usage: &lt;MudThemeProvider Theme="_theme" /&gt; with <c>private readonly MudTheme _theme = new PokeStreamTheme();</c>
/// </summary>
public class PokeStreamTheme : MudTheme
{
    public PokeStreamTheme()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#FF7A1A",     // Charmander orange
            PrimaryContrastText = "#2B1B0E",
            Secondary = "#2E6BFF",   // Pokédex blue
            SecondaryContrastText = "#FFFFFF",
            Tertiary = "#FFD60A",    // Pikachu yellow
            TertiaryContrastText = "#2B1B0E",
            TextPrimary = "#2B1B0E",
            Background = "#FBE9D6",
            Surface = "#FFFFFF",
            AppbarBackground = "#FF7A1A",
            AppbarText = "#2B1B0E",
            DrawerBackground = "#F6D9BC"
        };

        PaletteDark = new PaletteDark
        {
            Primary = "#3B55E6",     // Squirtle night blue
            PrimaryContrastText = "#FFFFFF",
            Secondary = "#FF8A2A",   // Orange spark
            SecondaryContrastText = "#1A0F05",
            Tertiary = "#FFD60A",    // Pikachu yellow
            TertiaryContrastText = "#1A0F05",
            TextPrimary = "#EEF1FF",
            Background = "#0A0F24",
            Surface = "#131A3A",
            AppbarBackground = "#101842",
            AppbarText = "#EEF1FF",
            DrawerBackground = "#0F1631"
        };

        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" }
            }
        };

        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "8px",
            DrawerWidthLeft = "240px",
            DrawerWidthRight = "240px",
            AppbarHeight = "64px"
        };

        Shadows = new Shadow();
        ZIndex = new ZIndex();
    }
}

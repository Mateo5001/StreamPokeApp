// PokéStream Collector - MudBlazor Theme Configuration
// Implements the PokéStream design system using MudBlazor's theming API

using MudBlazor;

namespace PokeStream.UI.Web.Theme;

/// <summary>
/// Custom MudBlazor theme implementing the PokéStream design system.
/// Maps design tokens from DESIGN-TOKENS.md to MudBlazor theme properties.
/// </summary>
public static class PokestreamTheme
{
    /// <summary>
    /// Creates the PokéStream dark theme (default for overlay).
    /// </summary>
    public static MudTheme CreateDarkTheme() => new MudTheme()
    {
        PaletteDark = new PaletteDark()
        {
            // Primary - Electric Yellow
            Primary = "#FFD700",
            PrimaryContrastText = "#000000",

            // Secondary - PokéBall Red
            Secondary = "#E53E3E",
            SecondaryContrastText = "#FFFFFF",

            // Tertiary - Grass Green (Success)
            Tertiary = "#3FB950",
            TertiaryContrastText = "#FFFFFF",

            // Error - Fair Red
            Error = "#F85149",
            ErrorContrastText = "#FFFFFF",

            // Warning - Fire Orange
            Warning = "#F78166",
            WarningContrastText = "#FFFFFF",

            // Info - Water Blue
            Info = "#58A6FF",
            InfoContrastText = "#FFFFFF",

            // Surface & Background
            Background = "#0D1117",
            Surface = "#161B22",

            // AppBar
            AppbarBackground = "#161B22",
            AppbarText = "#E6EDF3",

            // Drawer
            DrawerBackground = "#161B22",
            DrawerText = "#E6EDF3",

            // Text
            TextPrimary = "#E6EDF3",
            TextSecondary = "#8B949E",

            // Divider
            Divider = "#30363D",

            // Lines
            LinesDefault = "#30363D",
            LinesInputs = "#30363D",

            // Gray scale
            GrayDefault = "#8B949E",
            GrayLight = "#4A4A6A",
            GrayLighter = "#30363D",
            GrayDark = "#161B22",
            GrayDarker = "#0D1117",

            // Action
            ActionDefault = "#8B949E",
            ActionDisabled = "rgba(139, 148, 158, 0.38)",
            ActionDisabledBackground = "rgba(139, 148, 158, 0.12)",
        },

        PaletteLight = new PaletteLight()
        {
            // Primary - Electric Yellow (darker for light mode)
            Primary = "#B8860B",
            PrimaryContrastText = "#FFFFFF",

            // Secondary - PokéBall Red
            Secondary = "#C62828",
            SecondaryContrastText = "#FFFFFF",

            // Tertiary - Grass Green
            Tertiary = "#28A745",
            TertiaryContrastText = "#FFFFFF",

            // Error
            Error = "#C62828",
            ErrorContrastText = "#FFFFFF",

            // Warning
            Warning = "#E65100",
            WarningContrastText = "#FFFFFF",

            // Info
            Info = "#0366D6",
            InfoContrastText = "#FFFFFF",

            // Surface & Background
            Background = "#FAFAFA",
            Surface = "#FFFFFF",

            // AppBar
            AppbarBackground = "#FFFFFF",
            AppbarText = "#1A1A2E",

            // Drawer
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1A1A2E",

            // Text
            TextPrimary = "#1A1A2E",
            TextSecondary = "#4A4A6A",

            // Divider
            Divider = "#D0D7DE",

            // Lines
            LinesDefault = "#D0D7DE",
            LinesInputs = "#D0D7DE",

            // Gray scale
            GrayDefault = "#4A4A6A",
            GrayLight = "#D0D7DE",
            GrayLighter = "#E1E4E8",
            GrayDark = "#FAFAFA",
            GrayDarker = "#F3F4F6",

            // Action
            ActionDefault = "#4A4A6A",
            ActionDisabled = "rgba(74, 74, 106, 0.38)",
            ActionDisabledBackground = "rgba(74, 74, 106, 0.12)",
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "8px",
            AppbarHeight = "64px",
            DrawerWidthLeft = "250px",
            DrawerWidthRight = "300px",
            DrawerMiniWidthLeft = "64px",
            DrawerMiniWidthRight = "64px",
        },

        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "1rem",
                FontWeight = "400",
                LineHeight = "1.6",
                LetterSpacing = "0",
            },

            H1 = new H1Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(2.5rem, 5vw, 4rem)",
                FontWeight = "700",
                LineHeight = "1.1",
                LetterSpacing = "-0.02em",
            },

            H2 = new H2Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(2rem, 4vw, 3rem)",
                FontWeight = "700",
                LineHeight = "1.15",
                LetterSpacing = "-0.01em",
            },

            H3 = new H3Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(1.5rem, 3vw, 2.25rem)",
                FontWeight = "600",
                LineHeight = "1.2",
                LetterSpacing = "-0.01em",
            },

            H4 = new H4Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(1.25rem, 2.5vw, 1.75rem)",
                FontWeight = "600",
                LineHeight = "1.3",
                LetterSpacing = "0",
            },

            H5 = new H5Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(1.125rem, 2vw, 1.5rem)",
                FontWeight = "600",
                LineHeight = "1.35",
                LetterSpacing = "0",
            },

            H6 = new H6Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(1rem, 1.5vw, 1.25rem)",
                FontWeight = "500",
                LineHeight = "1.4",
                LetterSpacing = "0",
            },

            Subtitle1 = new Subtitle1Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(0.875rem, 1vw, 1rem)",
                FontWeight = "500",
                LineHeight = "1.45",
                LetterSpacing = "0.01em",
            },

            Subtitle2 = new Subtitle2Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "0.875rem",
                FontWeight = "500",
                LineHeight = "1.5",
                LetterSpacing = "0.01em",
            },

            Body1 = new Body1Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "1rem",
                FontWeight = "400",
                LineHeight = "1.6",
                LetterSpacing = "0",
            },

            Body2 = new Body2Typography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.5",
                LetterSpacing = "0.01em",
            },

            Button = new ButtonTypography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "1rem",
                FontWeight = "500",
                LineHeight = "1.5",
                LetterSpacing = "0",
                TextTransform = "none",
            },

            Caption = new CaptionTypography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(0.625rem, 0.6vw, 0.75rem)",
                FontWeight = "400",
                LineHeight = "1.4",
                LetterSpacing = "0.02em",
            },

            Overline = new OverlineTypography()
            {
                FontFamily = new[] { "Inter Variable", "Inter", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "sans-serif" },
                FontSize = "clamp(0.625rem, 0.6vw, 0.75rem)",
                FontWeight = "500",
                LineHeight = "1.4",
                LetterSpacing = "0.08em",
                TextTransform = "uppercase",
            },
        },

        ZIndex = new ZIndex()
        {
            AppBar = 200,
            Drawer = 300,
            Snackbar = 500,
            Tooltip = 600,
        },
    };

    /// <summary>
    /// Creates the PokéStream light theme.
    /// </summary>
    public static MudTheme CreateLightTheme() => new MudTheme()
    {
        PaletteLight = CreateDarkTheme().PaletteLight,
        LayoutProperties = CreateDarkTheme().LayoutProperties,
        Typography = CreateDarkTheme().Typography,
        ZIndex = CreateDarkTheme().ZIndex,
    };

    /// <summary>
    /// Default theme (dark mode as default for overlay).
    /// </summary>
    public static MudTheme Default => CreateDarkTheme();
}
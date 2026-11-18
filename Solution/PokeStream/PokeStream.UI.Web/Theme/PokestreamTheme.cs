// PokéStream Collector - MudBlazor Theme Configuration
// Implements the PokéStream design system using MudBlazor's theming API

using MudBlazor;

namespace PokeStream.UI.Web.Theme;

/// <summary>
/// Custom MudBlazor theme implementing the PokéStream design system.
/// Maps design tokens from DESIGN-TOKENS.md to MudBlazor theme properties.
/// Theme: Night (default) / Day — Primary: Orange, Secondary: Dark Blue
/// </summary>
public static class PokestreamTheme
{
    /// <summary>
    /// Creates the PokéStream dark theme (default for overlay).
    /// Night Theme: Primary Orange (#FF8C00), Secondary Dark Blue (#0D1B2A)
    /// </summary>
    public static MudTheme CreateDarkTheme() => new MudTheme()
    {
        PaletteDark = new PaletteDark()
        {
            // Primary - Orange
            Primary = "#FF8C00",
            PrimaryContrastText = "#000000",

            // Secondary - Dark Blue
            Secondary = "#0D1B2A",
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
            Background = "#0A0E17",
            Surface = "#111827",

            // AppBar
            AppbarBackground = "#111827",
            AppbarText = "#E6EDF3",

            // Drawer
            DrawerBackground = "#111827",
            DrawerText = "#E6EDF3",

            // Text
            TextPrimary = "#E6EDF3",
            TextSecondary = "#94A3B8",

            // Divider
            Divider = "#334155",

            // Lines
            LinesDefault = "#334155",
            LinesInputs = "#334155",

            // Gray scale
            GrayDefault = "#94A3B8",
            GrayLight = "#475569",
            GrayLighter = "#334155",
            GrayDark = "#111827",
            GrayDarker = "#0A0E17",

            // Action
            ActionDefault = "#94A3B8",
            ActionDisabled = "rgba(148, 163, 184, 0.38)",
            ActionDisabledBackground = "rgba(148, 163, 184, 0.12)",
        },

        PaletteLight = new PaletteLight()
        {
            // Primary - Vibrant Orange (accessible on light backgrounds, 4.5:1 contrast on white)
            Primary = "#E86C00",
            PrimaryContrastText = "#FFFFFF",

            // Secondary - Warm Dark Blue
            Secondary = "#0D1B2A",
            SecondaryContrastText = "#FFFFFF",

            // Tertiary - Grass Green
            Tertiary = "#28A745",
            TertiaryContrastText = "#FFFFFF",

            // Error
            Error = "#C62828",
            ErrorContrastText = "#FFFFFF",

            // Warning - Fire Orange
            Warning = "#E65100",
            WarningContrastText = "#FFFFFF",

            // Info - Water Blue
            Info = "#0366D6",
            InfoContrastText = "#FFFFFF",

            // Surface & Background - Warm, not sterile
            Background = "#FFFDF8",
            Surface = "#FFFFFF",

            // AppBar - subtle warmth
            AppbarBackground = "#FFFFFF",
            AppbarText = "#0F172A",

            // Drawer
            DrawerBackground = "#FFFFFF",
            DrawerText = "#0F172A",

            // Text - High contrast, warm grays
            TextPrimary = "#0F172A",
            TextSecondary = "#475569",

            // Divider
            Divider = "#E2E8F0",

            // Lines
            LinesDefault = "#E2E8F0",
            LinesInputs = "#CBD5E1",

            // Gray scale - warm grays
            GrayDefault = "#64748B",
            GrayLight = "#CBD5E1",
            GrayLighter = "#E2E8F0",
            GrayDark = "#FFFDF8",
            GrayDarker = "#F8F5EE",

            // Action
            ActionDefault = "#64748B",
            ActionDisabled = "rgba(100, 116, 139, 0.38)",
            ActionDisabledBackground = "rgba(100, 116, 139, 0.12)",
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "12px",
            AppbarHeight = "64px",
            DrawerWidthLeft = "280px",
            DrawerWidthRight = "320px",
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
    /// Default theme (dark mode as default for overlay).
    /// Contains both PaletteDark and PaletteLight for automatic switching via data-mud-theme attribute.
    /// </summary>
    public static MudTheme Default => CreateDarkTheme();
}
# Navigation Icons

Custom SVG icons for the NavMenu, following the PokéStream design system.

## Icon Specifications

| Property | Value |
|----------|-------|
| **Grid** | 24×24px |
| **Stroke** | 2px (consistent weight) |
| **Style** | Outline/rounded (Phosphor-style) |
| **Color** | `currentColor` (inherits text color) |
| **Fill** | `none` (except accent circles) |

## Icons

| File | Nav Item | Description |
|------|----------|-------------|
| `nav-home.svg` | Inicio | House with door |
| `nav-collection.svg` | Mi Colección | PokéBall-inspired circle |
| `nav-trade.svg` | Intercambios | Two-way swap with PokéBalls |
| `nav-leaderboard.svg` | Ranking | Bar chart with podium circles |
| `nav-profile.svg` | Perfil | User silhouette |
| `nav-settings.svg` | Configuración | Gear/cog |

## Usage in Blazor

### Option 1: Inline SVG Component (Recommended)

```razor
<!-- Shared/NavIcon.razor -->
<svg class="nav-icon @Class" aria-hidden="true">
    @IconMarkup
</svg>

@code {
    [Parameter] public string Name { get; set; } // "home", "collection", etc.
    [Parameter] public string Class { get; set; }
    
    private MarkupString IconMarkup => GetIcon(Name);
    
    private MarkupString GetIcon(string name) => name switch {
        "home" => (MarkupString)@"<path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"/><polyline points=\"9 22 9 12 15 12 15 22\"/>",
        "collection" => (MarkupString)@"<circle cx=\"12\" cy=\"12\" r=\"10\"/><line x1=\"12\" y1=\"2\" x2=\"12\" y2=\"22\"/><path d=\"M2 12a10 10 0 0 1 20 0\"/><circle cx=\"12\" cy=\"12\" r=\"3\" fill=\"currentColor\" opacity=\"0.3\"/>",
        "trade" => (MarkupString)@"<path d=\"M7 16l9-9\"/><path d=\"M16 7l-9 9\"/><circle cx=\"7\" cy=\"16\" r=\"3\"/><circle cx=\"17\" cy=\"6\" r=\"3\"/>",
        "leaderboard" => (MarkupString)@"<path d=\"M12 5v14\"/><path d=\"M18 12v7\"/><path d=\"M6 12v4\"/><path d=\"M4 19h16\"/><circle cx=\"12\" cy=\"5\" r=\"2\" fill=\"currentColor\"/><circle cx=\"18\" cy=\"12\" r=\"2\" fill=\"currentColor\" opacity=\"0.5\"/><circle cx=\"6\" cy=\"12\" r=\"2\" fill=\"currentColor\" opacity=\"0.3\"/>",
        "profile" => (MarkupString)@"<path d=\"M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2\"/><circle cx=\"12\" cy=\"7\" r=\"4\"/>",
        "settings" => (MarkupString)@"<circle cx=\"12\" cy=\"12\" r=\"3\"/><path d=\"M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06a2 2 0 0 1 0 2.83 2 2 0 0 1-2.83 0l-.06-.06a1.65 1.65 0 0 0-1.82-.33 1.65 1.65 0 0 0-1 1.51V21a2 2 0 0 1-2 2 2 2 0 0 1-2-2v-.09A1.65 1.65 0 0 0 9 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06a2 2 0 0 1-2.83 0 2 2 0 0 1 0-2.83l.06-.06a1.65 1.65 0 0 0 .33-1.82 1.65 1.65 0 0 0-1.51-1H3a2 2 0 0 1-2-2 2 2 0 0 1 2-2h.09A1.65 1.65 0 0 0 4.6 9a1.65 1.65 0 0 0-.33-1.82l-.06-.06a2 2 0 0 1 0-2.83 2 2 0 0 1 2.83 0l.06.06a1.65 1.65 0 0 0 1.82.33H9a1.65 1.65 0 0 0 1-1.51V3a2 2 0 0 1 2-2 2 2 0 0 1 2 2v.09a1.65 1.65 0 0 0 1 1.51 1.65 1.65 0 0 0 1.82-.33l.06-.06a2 2 0 0 1 2.83 0 2 2 0 0 1 0 2.83l-.06.06a1.65 1.65 0 0 0-.33 1.82V9a1.65 1.65 0 0 0 1.51 1H21a2 2 0 0 1 2 2 2 2 0 0 1-2 2h-.09a1.65 1.65 0 0 0-1.51 1z\"/>",
        _ => (MarkupString)""
    };
}
```

### Option 2: CSS Mask (For external SVG files)

```css
/* wwwroot/css/nav-icons.css */
.nav-icon {
  width: 24px;
  height: 24px;
  display: inline-block;
  vertical-align: middle;
  flex-shrink: 0;
}

.nav-icon--home { mask-image: url('/icons/nav-home.svg'); }
.nav-icon--collection { mask-image: url('/icons/nav-collection.svg'); }
.nav-icon--trade { mask-image: url('/icons/nav-trade.svg'); }
.nav-icon--leaderboard { mask-image: url('/icons/nav-leaderboard.svg'); }
.nav-icon--profile { mask-image: url('/icons/nav-profile.svg'); }
.nav-icon--settings { mask-image: url('/icons/nav-settings.svg'); }

/* Dark mode: icons inherit text color via mask */
[data-theme="dark"] .nav-icon {
  background-color: var(--color-on-surface);
}

[data-theme="light"] .nav-icon {
  background-color: var(--color-on-surface);
}
```

```razor
<!-- Usage in NavMenu.razor -->
<MudNavLink Href="/" Match="NavLinkMatch.All" Icon="@null">
    <span class="nav-icon nav-icon--home" aria-hidden="true"></span>
    Inicio
</MudNavLink>
```

## Usage in MAUI

```xml
<!-- Resources/Styles/Icons.xaml -->
<ResourceDictionary>
    <OnPlatform x:Key="NavHomeIcon" x:TypeArguments="ImageSource">
        <On Platform="WinUI, iOS, Android, MacCatalyst" Value="nav_home.svg" />
    </OnPlatform>
    <!-- ... repeat for others -->
</ResourceDictionary>
```

```xml
<!-- In NavMenu or Shell -->
<Image Source="{StaticResource NavHomeIcon}" 
       WidthRequest="24" 
       HeightRequest="24" 
       VerticalOptions="Center" />
```

## Design Tokens Reference

These icons use the following tokens from `DESIGN-TOKENS.md`:

```json
{
  "icon": {
    "size": { "value": "24px", "type": "dimension" },
    "strokeWidth": { "value": "2px", "type": "dimension" },
    "strokeColor": { "value": "currentColor", "type": "color" }
  }
}
```

CSS Variables:
```css
:root {
  --icon-size: 24px;
  --icon-stroke-width: 2px;
  --icon-stroke-color: currentColor;
}
```

## Accessibility

- All icons have `aria-hidden="true"` (decorative)
- Parent `<MudNavLink>` provides accessible name via text content
- Icons inherit `currentColor` for high contrast mode support
- Minimum 24×24px touch target (meets WCAG 2.5.5)

## Export Checklist

- [ ] SVGs optimized (SVGO)
- [ ] `currentColor` used for stroke
- [ ] No hardcoded colors
- [ ] ViewBox 0 0 24 24
- [ ] Stroke-width: 2
- [ ] Stroke-linecap: round
- [ ] Stroke-linejoin: round
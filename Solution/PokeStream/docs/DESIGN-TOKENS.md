# PokéStream Collector - Design Tokens Reference

> **Single Source of Truth**: All design decisions codified as tokens. Consumed by Blazor (CSS variables), MAUI (ResourceDictionary), and design tools (Figma Tokens plugin).

---

## Token Structure

```json
{
  "color": { ... },
  "spacing": { ... },
  "borderRadius": { ... },
  "shadow": { ... },
  "typography": { ... },
  "motion": { ... },
  "zIndex": { ... },
  "breakpoints": { ... },
  "opacity": { ... }
}
```

---

## Complete Token Definitions

### Color Tokens

```json
{
  "color": {
    "primary": {
      "value": "#FFD700",
      "type": "color",
      "description": "Electric Yellow - Primary brand, CTAs, active states",
      "dark": "#FFD700",
      "light": "#B8860B"
    },
    "primaryHover": {
      "value": "#FFE066",
      "type": "color",
      "description": "Primary hover/pressed state",
      "dark": "#FFE066",
      "light": "#967009"
    },
    "primaryContainer": {
      "value": "#3D3500",
      "type": "color",
      "description": "Primary filled backgrounds",
      "dark": "#3D3500",
      "light": "#FFF8E1"
    },
    "onPrimary": {
      "value": "#000000",
      "type": "color",
      "description": "Text/icons on primary"
    },
    "secondary": {
      "value": "#E53E3E",
      "type": "color",
      "description": "PokéBall Red - Capture, accents, destructive",
      "dark": "#E53E3E",
      "light": "#C62828"
    },
    "secondaryHover": {
      "value": "#FC8181",
      "type": "color",
      "description": "Secondary hover/pressed",
      "dark": "#FC8181",
      "light": "#B71C1C"
    },
    "secondaryContainer": {
      "value": "#3D1A1A",
      "type": "color",
      "description": "Secondary filled backgrounds",
      "dark": "#3D1A1A",
      "light": "#FDEDEC"
    },
    "onSecondary": {
      "value": "#FFFFFF",
      "type": "color",
      "description": "Text/icons on secondary"
    },
    "tertiary": {
      "value": "#3FB950",
      "type": "color",
      "description": "Grass Green - Success, verified, online",
      "dark": "#3FB950",
      "light": "#28A745"
    },
    "tertiaryHover": {
      "value": "#56D364",
      "type": "color",
      "description": "Tertiary hover",
      "dark": "#56D364",
      "light": "#2ECC71"
    },
    "tertiaryContainer": {
      "value": "#0C2D13",
      "type": "color",
      "description": "Tertiary filled backgrounds",
      "dark": "#0C2D13",
      "light": "#E6F4EA"
    },
    "onTertiary": {
      "value": "#FFFFFF",
      "type": "color",
      "description": "Text/icons on tertiary"
    },
    "error": {
      "value": "#F85149",
      "type": "color",
      "description": "Fair Red - Errors, failed states",
      "dark": "#F85149",
      "light": "#C62828"
    },
    "errorHover": {
      "value": "#FF7B72",
      "type": "color",
      "description": "Error hover",
      "dark": "#FF7B72",
      "light": "#B71C1C"
    },
    "errorContainer": {
      "value": "#3D1A1A",
      "type": "color",
      "description": "Error filled backgrounds",
      "dark": "#3D1A1A",
      "light": "#FDEDEC"
    },
    "onError": {
      "value": "#FFFFFF",
      "type": "color",
      "description": "Text/icons on error"
    },
    "warning": {
      "value": "#F78166",
      "type": "color",
      "description": "Fire Orange - Warnings, cooldowns, pending",
      "dark": "#F78166",
      "light": "#E65100"
    },
    "info": {
      "value": "#58A6FF",
      "type": "color",
      "description": "Water Blue - Info, links, water-type",
      "dark": "#58A6FF",
      "light": "#0366D6"
    },
    "background": {
      "value": "#0D1117",
      "type": "color",
      "description": "Page/app background (dark default)",
      "dark": "#0D1117",
      "light": "#FAFAFA"
    },
    "onBackground": {
      "value": "#E6EDF3",
      "type": "color",
      "description": "Primary text on background",
      "dark": "#E6EDF3",
      "light": "#1A1A2E"
    },
    "surface": {
      "value": "#161B22",
      "type": "color",
      "description": "Cards, panels, sheets",
      "dark": "#161B22",
      "light": "#FFFFFF"
    },
    "onSurface": {
      "value": "#E6EDF3",
      "type": "color",
      "description": "Primary text on surface",
      "dark": "#E6EDF3",
      "light": "#1A1A2E"
    },
    "surfaceVariant": {
      "value": "#21262D",
      "type": "color",
      "description": "Subtle backgrounds, hover states",
      "dark": "#21262D",
      "light": "#F6F8FA"
    },
    "onSurfaceVariant": {
      "value": "#8B949E",
      "type": "color",
      "description": "Secondary text, labels, placeholders",
      "dark": "#8B949E",
      "light": "#4A4A6A"
    },
    "surfaceContainer": {
      "value": "#1F2428",
      "type": "color",
      "description": "Elevated containers",
      "dark": "#1F2428",
      "light": "#F3F4F6"
    },
    "outline": {
      "value": "#30363D",
      "type": "color",
      "description": "Borders, dividers, focus rings",
      "dark": "#30363D",
      "light": "#D0D7DE"
    },
    "outlineVariant": {
      "value": "#21262D",
      "type": "color",
      "description": "Subtle borders, input borders",
      "dark": "#21262D",
      "light": "#E1E4E8"
    },
    "platform": {
      "twitch": { "value": "#9146FF", "type": "color", "description": "Twitch brand" },
      "youtube": { "value": "#FF0000", "type": "color", "description": "YouTube brand" },
      "kick": { "value": "#00FF7F", "type": "color", "description": "Kick brand" },
      "discord": { "value": "#5865F2", "type": "color", "description": "Discord brand" }
    },
    "rarity": {
      "common": { "value": "#8B949E", "type": "color", "description": "Common - Gray" },
      "uncommon": { "value": "#3FB950", "type": "color", "description": "Uncommon - Green" },
      "rare": { "value": "#58A6FF", "type": "color", "description": "Rare - Blue" },
      "epic": { "value": "#A371F7", "type": "color", "description": "Epic - Purple" },
      "legendary": { "value": "#F78166", "type": "color", "description": "Legendary - Orange" },
      "mythical": { "value": "#FFD700", "type": "color", "description": "Mythical - Gold" },
      "shiny": { "value": "#FF85C1", "type": "color", "description": "Shiny - Pink" }
    },
    "type": {
      "normal": { "value": "#A8A878", "on": "#000000" },
      "fire": { "value": "#F08030", "on": "#FFFFFF" },
      "water": { "value": "#6890F0", "on": "#FFFFFF" },
      "electric": { "value": "#F8D030", "on": "#000000" },
      "grass": { "value": "#78C850", "on": "#FFFFFF" },
      "ice": { "value": "#98D8D8", "on": "#000000" },
      "fighting": { "value": "#C03028", "on": "#FFFFFF" },
      "poison": { "value": "#A040A0", "on": "#FFFFFF" },
      "ground": { "value": "#E0C068", "on": "#000000" },
      "flying": { "value": "#A890F0", "on": "#FFFFFF" },
      "psychic": { "value": "#F85888", "on": "#FFFFFF" },
      "bug": { "value": "#A8B820", "on": "#000000" },
      "rock": { "value": "#B8A038", "on": "#FFFFFF" },
      "ghost": { "value": "#705898", "on": "#FFFFFF" },
      "dragon": { "value": "#7038F8", "on": "#FFFFFF" },
      "dark": { "value": "#705848", "on": "#FFFFFF" },
      "steel": { "value": "#B8B8D0", "on": "#000000" },
      "fairy": { "value": "#EE99AC", "on": "#000000" },
      "stellar": { "value": "#FFD700", "on": "#000000" }
    },
    "gradient": {
      "primary": { "value": "linear-gradient(135deg, #FFD700 0%, #FFB300 50%, #FF8F00 100%)", "type": "gradient" },
      "capture": { "value": "linear-gradient(180deg, #FC8181 0%, #E53E3E 50%, #C62828 100%)", "type": "gradient" },
      "rare": { "value": "radial-gradient(circle at center, rgba(255,215,0,0.4) 0%, transparent 70%)", "type": "gradient" },
      "mythical": { "value": "linear-gradient(90deg, #FFD700, #FFA500, #FFD700, #FF69B4, #FFD700)", "type": "gradient" }
    }
  }
}
```

### Spacing Tokens

```json
{
  "spacing": {
    "0": { "value": "0", "type": "dimension", "description": "No spacing" },
    "1": { "value": "4px", "type": "dimension", "description": "Extra tight" },
    "2": { "value": "8px", "type": "dimension", "description": "Tight / default gap" },
    "3": { "value": "12px", "type": "dimension", "description": "Component padding" },
    "4": { "value": "16px", "type": "dimension", "description": "Standard padding" },
    "5": { "value": "20px", "type": "dimension", "description": "Generous padding" },
    "6": { "value": "24px", "type": "dimension", "description": "Section spacing" },
    "8": { "value": "32px", "type": "dimension", "description": "Large section spacing" },
    "10": { "value": "40px", "type": "dimension", "description": "Page margins" },
    "12": { "value": "48px", "type": "dimension", "description": "Hero spacing" },
    "16": { "value": "64px", "type": "dimension", "description": "Major breaks" }
  }
}
```

### Border Radius Tokens

```json
{
  "borderRadius": {
    "none": { "value": "0", "type": "dimension", "description": "Sharp corners" },
    "sm": { "value": "4px", "type": "dimension", "description": "Badges, tags, chips" },
    "md": { "value": "8px", "type": "dimension", "description": "Buttons, inputs, cards" },
    "lg": { "value": "12px", "type": "dimension", "description": "Modals, panels, dropdowns" },
    "xl": { "value": "16px", "type": "dimension", "description": "Large containers, sheets" },
    "full": { "value": "9999px", "type": "dimension", "description": "Pills, avatars, progress rings" }
  }
}
```

### Shadow Tokens

```json
{
  "shadow": {
    "none": { "value": "none", "type": "shadow", "description": "No shadow" },
    "sm": { "value": "0 1px 2px rgba(0,0,0,0.3)", "type": "shadow", "description": "Cards, default elevation" },
    "md": { "value": "0 4px 8px rgba(0,0,0,0.4)", "type": "shadow", "description": "Raised cards, dropdowns" },
    "lg": { "value": "0 8px 24px rgba(0,0,0,0.5)", "type": "shadow", "description": "Modals, sheets" },
    "xl": { "value": "0 16px 48px rgba(0,0,0,0.6)", "type": "shadow", "description": "Toasts, popovers" },
    "glow": { "value": "0 0 24px rgba(255,215,0,0.3)", "type": "shadow", "description": "Primary CTAs, rare spawns" },
    "glowStrong": { "value": "0 0 32px rgba(255,215,0,0.5), 0 0 64px rgba(255,215,0,0.3)", "type": "shadow", "description": "Mythical/shiny celebration" }
  }
}
```

### Typography Tokens

```json
{
  "typography": {
    "fontFamily": {
      "base": { "value": "'Inter Variable', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif", "type": "fontFamily" },
      "mono": { "value": "'JetBrains Mono Variable', 'JetBrains Mono', 'Fira Code', monospace", "type": "fontFamily" },
      "display": { "value": "'Press Start 2P', 'VT323', monospace", "type": "fontFamily" }
    },
    "fontWeight": {
      "thin": { "value": "100", "type": "fontWeight" },
      "light": { "value": "300", "type": "fontWeight" },
      "regular": { "value": "400", "type": "fontWeight" },
      "medium": { "value": "500", "type": "fontWeight" },
      "semibold": { "value": "600", "type": "fontWeight" },
      "bold": { "value": "700", "type": "fontWeight" },
      "extrabold": { "value": "800", "type": "fontWeight" }
    },
    "fontSize": {
      "displayXL": { "value": "clamp(2.5rem, 5vw, 4rem)", "type": "fontSize" },
      "displayLG": { "value": "clamp(2rem, 4vw, 3rem)", "type": "fontSize" },
      "displayMD": { "value": "clamp(1.5rem, 3vw, 2.25rem)", "type": "fontSize" },
      "headingXL": { "value": "clamp(1.25rem, 2.5vw, 1.75rem)", "type": "fontSize" },
      "headingLG": { "value": "clamp(1.125rem, 2vw, 1.5rem)", "type": "fontSize" },
      "headingMD": { "value": "clamp(1rem, 1.5vw, 1.25rem)", "type": "fontSize" },
      "headingSM": { "value": "clamp(0.875rem, 1vw, 1rem)", "type": "fontSize" },
      "bodyLG": { "value": "clamp(1rem, 1.5vw, 1.25rem)", "type": "fontSize" },
      "bodyMD": { "value": "clamp(0.875rem, 1vw, 1rem)", "type": "fontSize" },
      "bodySM": { "value": "clamp(0.75rem, 0.8vw, 0.875rem)", "type": "fontSize" },
      "caption": { "value": "clamp(0.625rem, 0.6vw, 0.75rem)", "type": "fontSize" },
      "overline": { "value": "clamp(0.625rem, 0.6vw, 0.75rem)", "type": "fontSize" }
    },
    "lineHeight": {
      "none": { "value": "1", "type": "lineHeight" },
      "tight": { "value": "1.1", "type": "lineHeight" },
      "snug": { "value": "1.25", "type": "lineHeight" },
      "normal": { "value": "1.5", "type": "lineHeight" },
      "relaxed": { "value": "1.6", "type": "lineHeight" },
      "loose": { "value": "1.75", "type": "lineHeight" },
      "code": { "value": "1.6", "type": "lineHeight" }
    },
    "letterSpacing": {
      "tighter": { "value": "-0.02em", "type": "letterSpacing" },
      "tight": { "value": "-0.01em", "type": "letterSpacing" },
      "normal": { "value": "0", "type": "letterSpacing" },
      "wide": { "value": "0.01em", "type": "letterSpacing" },
      "wider": { "value": "0.02em", "type": "letterSpacing" },
      "widest": { "value": "0.08em", "type": "letterSpacing" }
    }
  }
}
```

### Motion Tokens

```json
{
  "motion": {
    "duration": {
      "instant": { "value": "0ms", "type": "duration" },
      "fast": { "value": "100ms", "type": "duration" },
      "normal": { "value": "200ms", "type": "duration" },
      "slow": { "value": "300ms", "type": "duration" },
      "slower": { "value": "500ms", "type": "duration" }
    },
    "easing": {
      "standard": { "value": "cubic-bezier(0.2, 0, 0.38, 0.9)", "type": "easing" },
      "spring": { "value": "cubic-bezier(0.34, 1.56, 0.64, 1)", "type": "easing" },
      "sharp": { "value": "cubic-bezier(0.4, 0, 1, 1)", "type": "easing" },
      "elastic": { "value": "cubic-bezier(0.175, 0.885, 0.32, 1.275)", "type": "easing" }
    }
  }
}
```

### Z-Index Tokens

```json
{
  "zIndex": {
    "base": { "value": "0", "type": "number", "description": "Base content" },
    "dropdown": { "value": "100", "type": "number", "description": "Dropdowns, menus" },
    "sticky": { "value": "200", "type": "number", "description": "Sticky headers, nav" },
    "overlay": { "value": "300", "type": "number", "description": "Modals, drawers" },
    "popover": { "value": "400", "type": "number", "description": "Popovers, tooltips" },
    "toast": { "value": "500", "type": "number", "description": "Toasts, notifications" },
    "tooltip": { "value": "600", "type": "number", "description": "Tooltips (highest)" },
    "overlayBg": { "value": "250", "type": "number", "description": "Modal backdrops" }
  }
}
```

### Breakpoint Tokens

```json
{
  "breakpoints": {
    "sm": { "value": "640px", "type": "breakpoint", "description": "Mobile landscape" },
    "md": { "value": "768px", "type": "breakpoint", "description": "Tablet portrait" },
    "lg": { "value": "1024px", "type": "breakpoint", "description": "Tablet landscape / small desktop" },
    "xl": { "value": "1280px", "type": "breakpoint", "description": "Desktop" },
    "2xl": { "value": "1536px", "type": "breakpoint", "description": "Large desktop" }
  }
}
```

### Opacity Tokens

```json
{
  "opacity": {
    "0": { "value": "0", "type": "number" },
    "5": { "value": "0.05", "type": "number" },
    "10": { "value": "0.1", "type": "number" },
    "20": { "value": "0.2", "type": "number" },
    "30": { "value": "0.3", "type": "number" },
    "40": { "value": "0.4", "type": "number" },
    "50": { "value": "0.5", "type": "number" },
    "60": { "value": "0.6", "type": "number" },
    "70": { "value": "0.7", "type": "number" },
    "80": { "value": "0.8", "type": "number" },
    "90": { "value": "0.9", "type": "number" },
    "100": { "value": "1", "type": "number" },
    "disabled": { "value": "0.38", "type": "number", "description": "Disabled interactive elements" },
    "hover": { "value": "0.08", "type": "number", "description": "Hover overlay" },
    "pressed": { "value": "0.12", "type": "number", "description": "Pressed overlay" },
    "focus": { "value": "0.16", "type": "number", "description": "Focus ring overlay" },
    "dragged": { "value": "0.5", "type": "number", "description": "Drag ghost opacity" }
  }
}
```

---

## Platform Consumption

### Blazor / Web (CSS Variables)

**File**: `wwwroot/css/tokens.css` (auto-generated from JSON)

```css
/* Auto-generated — do not edit directly */
:root {
  /* Color */
  --color-primary: #FFD700;
  --color-primary-hover: #FFE066;
  --color-primary-container: #3D3500;
  --color-on-primary: #000000;
  --color-secondary: #E53E3E;
  --color-secondary-hover: #FC8181;
  --color-secondary-container: #3D1A1A;
  --color-on-secondary: #FFFFFF;
  --color-tertiary: #3FB950;
  --color-tertiary-hover: #56D364;
  --color-tertiary-container: #0C2D13;
  --color-on-tertiary: #FFFFFF;
  --color-error: #F85149;
  --color-error-hover: #FF7B72;
  --color-error-container: #3D1A1A;
  --color-on-error: #FFFFFF;
  --color-warning: #F78166;
  --color-info: #58A6FF;
  --color-background: #0D1117;
  --color-on-background: #E6EDF3;
  --color-surface: #161B22;
  --color-on-surface: #E6EDF3;
  --color-surface-variant: #21262D;
  --color-on-surface-variant: #8B949E;
  --color-surface-container: #1F2428;
  --color-outline: #30363D;
  --color-outline-variant: #21262D;

  /* Spacing */
  --space-0: 0;
  --space-1: 4px;
  --space-2: 8px;
  --space-3: 12px;
  --space-4: 16px;
  --space-5: 20px;
  --space-6: 24px;
  --space-8: 32px;
  --space-10: 40px;
  --space-12: 48px;
  --space-16: 64px;

  /* Border Radius */
  --radius-none: 0;
  --radius-sm: 4px;
  --radius-md: 8px;
  --radius-lg: 12px;
  --radius-xl: 16px;
  --radius-full: 9999px;

  /* Shadows */
  --shadow-none: none;
  --shadow-sm: 0 1px 2px rgba(0,0,0,0.3);
  --shadow-md: 0 4px 8px rgba(0,0,0,0.4);
  --shadow-lg: 0 8px 24px rgba(0,0,0,0.5);
  --shadow-xl: 0 16px 48px rgba(0,0,0,0.6);
  --shadow-glow: 0 0 24px rgba(255,215,0,0.3);

  /* Typography */
  --font-family-base: 'Inter Variable', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
  --font-family-mono: 'JetBrains Mono Variable', 'JetBrains Mono', 'Fira Code', monospace;
  --font-family-display: 'Press Start 2P', 'VT323', monospace;

  --weight-thin: 100;
  --weight-light: 300;
  --weight-regular: 400;
  --weight-medium: 500;
  --weight-semibold: 600;
  --weight-bold: 700;
  --weight-extrabold: 800;

  --text-display-xl: clamp(2.5rem, 5vw, 4rem);
  --text-display-lg: clamp(2rem, 4vw, 3rem);
  --text-display-md: clamp(1.5rem, 3vw, 2.25rem);
  --text-heading-xl: clamp(1.25rem, 2.5vw, 1.75rem);
  --text-heading-lg: clamp(1.125rem, 2vw, 1.5rem);
  --text-heading-md: clamp(1rem, 1.5vw, 1.25rem);
  --text-heading-sm: clamp(0.875rem, 1vw, 1rem);
  --text-body-lg: clamp(1rem, 1.5vw, 1.25rem);
  --text-body-md: clamp(0.875rem, 1vw, 1rem);
  --text-body-sm: clamp(0.75rem, 0.8vw, 0.875rem);
  --text-caption: clamp(0.625rem, 0.6vw, 0.75rem);
  --text-overline: clamp(0.625rem, 0.6vw, 0.75rem);

  --leading-none: 1;
  --leading-tight: 1.1;
  --leading-snug: 1.25;
  --leading-normal: 1.5;
  --leading-relaxed: 1.6;
  --leading-loose: 1.75;
  --leading-code: 1.6;

  --tracking-tighter: -0.02em;
  --tracking-tight: -0.01em;
  --tracking-normal: 0;
  --tracking-wide: 0.01em;
  --tracking-wider: 0.02em;
  --tracking-widest: 0.08em;

  /* Motion */
  --duration-instant: 0ms;
  --duration-fast: 100ms;
  --duration-normal: 200ms;
  --duration-slow: 300ms;
  --duration-slower: 500ms;

  --ease-standard: cubic-bezier(0.2, 0, 0.38, 0.9);
  --ease-spring: cubic-bezier(0.34, 1.56, 0.64, 1);
  --ease-sharp: cubic-bezier(0.4, 0, 1, 1);
  --ease-elastic: cubic-bezier(0.175, 0.885, 0.32, 1.275);

  /* Z-Index */
  --z-base: 0;
  --z-dropdown: 100;
  --z-sticky: 200;
  --z-overlay: 300;
  --z-popover: 400;
  --z-toast: 500;
  --z-tooltip: 600;
  --z-overlay-bg: 250;

  /* Breakpoints (for container queries) */
  --bp-sm: 640px;
  --bp-md: 768px;
  --bp-lg: 1024px;
  --bp-xl: 1280px;
  --bp-2xl: 1536px;

  /* Opacity */
  --opacity-0: 0;
  --opacity-10: 0.1;
  --opacity-20: 0.2;
  --opacity-30: 0.3;
  --opacity-40: 0.4;
  --opacity-50: 0.5;
  --opacity-60: 0.6;
  --opacity-70: 0.7;
  --opacity-80: 0.8;
  --opacity-90: 0.9;
  --opacity-100: 1;
  --opacity-disabled: 0.38;
  --opacity-hover: 0.08;
  --opacity-pressed: 0.12;
  --opacity-focus: 0.16;
  --opacity-dragged: 0.5;
}

/* Light mode overrides */
@media (prefers-color-scheme: light) {
  :root:not([data-theme="dark"]) {
    --color-primary: #B8860B;
    --color-primary-hover: #967009;
    --color-primary-container: #FFF8E1;
    --color-on-primary: #FFFFFF;
    --color-secondary: #C62828;
    --color-secondary-hover: #B71C1C;
    --color-secondary-container: #FDEDEC;
    --color-on-secondary: #FFFFFF;
    --color-tertiary: #28A745;
    --color-tertiary-hover: #2ECC71;
    --color-tertiary-container: #E6F4EA;
    --color-on-tertiary: #FFFFFF;
    --color-error: #C62828;
    --color-error-hover: #B71C1C;
    --color-error-container: #FDEDEC;
    --color-on-error: #FFFFFF;
    --color-warning: #E65100;
    --color-info: #0366D6;
    --color-background: #FAFAFA;
    --color-on-background: #1A1A2E;
    --color-surface: #FFFFFF;
    --color-on-surface: #1A1A2E;
    --color-surface-variant: #F6F8FA;
    --color-on-surface-variant: #4A4A6A;
    --color-surface-container: #F3F4F6;
    --color-outline: #D0D7DE;
    --color-outline-variant: #E1E4E8;
  }
}

/* Forced themes */
[data-theme="dark"] { /* dark tokens */ }
[data-theme="light"] { /* light tokens */ }
```

### MAUI (ResourceDictionary)

**File**: `Resources/Styles/Tokens.xaml` (auto-generated)

```xml
<!-- Auto-generated — do not edit directly -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                    xmlns:sys="clr-namespace:System;assembly=netstandard">

  <!-- Color -->
  <Color x:Key="ColorPrimary">#FFD700</Color>
  <Color x:Key="ColorPrimaryHover">#FFE066</Color>
  <Color x:Key="ColorPrimaryContainer">#3D3500</Color>
  <Color x:Key="ColorOnPrimary">#000000</Color>

  <Color x:Key="ColorSecondary">#E53E3E</Color>
  <Color x:Key="ColorSecondaryHover">#FC8181</Color>
  <Color x:Key="ColorSecondaryContainer">#3D1A1A</Color>
  <Color x:Key="ColorOnSecondary">#FFFFFF</Color>

  <Color x:Key="ColorTertiary">#3FB950</Color>
  <Color x:Key="ColorTertiaryHover">#56D364</Color>
  <Color x:Key="ColorTertiaryContainer">#0C2D13</Color>
  <Color x:Key="ColorOnTertiary">#FFFFFF</Color>

  <Color x:Key="ColorError">#F85149</Color>
  <Color x:Key="ColorErrorHover">#FF7B72</Color>
  <Color x:Key="ColorErrorContainer">#3D1A1A</Color>
  <Color x:Key="ColorOnError">#FFFFFF</Color>

  <Color x:Key="ColorWarning">#F78166</Color>
  <Color x:Key="ColorInfo">#58A6FF</Color>

  <Color x:Key="ColorBackground">#0D1117</Color>
  <Color x:Key="ColorOnBackground">#E6EDF3</Color>
  <Color x:Key="ColorSurface">#161B22</Color>
  <Color x:Key="ColorOnSurface">#E6EDF3</Color>
  <Color x:Key="ColorSurfaceVariant">#21262D</Color>
  <Color x:Key="ColorOnSurfaceVariant">#8B949E</Color>
  <Color x:Key="ColorSurfaceContainer">#1F2428</Color>

  <Color x:Key="ColorOutline">#30363D</Color>
  <Color x:Key="ColorOutlineVariant">#21262D</Color>

  <!-- Platform Colors -->
  <Color x:Key="ColorPlatformTwitch">#9146FF</Color>
  <Color x:Key="ColorPlatformYouTube">#FF0000</Color>
  <Color x:Key="ColorPlatformKick">#00FF7F</Color>
  <Color x:Key="ColorPlatformDiscord">#5865F2</Color>

  <!-- Rarity Colors -->
  <Color x:Key="ColorRarityCommon">#8B949E</Color>
  <Color x:Key="ColorRarityUncommon">#3FB950</Color>
  <Color x:Key="ColorRarityRare">#58A6FF</Color>
  <Color x:Key="ColorRarityEpic">#A371F7</Color>
  <Color x:Key="ColorRarityLegendary">#F78166</Color>
  <Color x:Key="ColorRarityMythical">#FFD700</Color>
  <Color x:Key="ColorRarityShiny">#FF85C1</Color>

  <!-- Type Colors (with On colors) -->
  <Color x:Key="ColorTypeNormal">#A8A878</Color>
  <Color x:Key="ColorOnTypeNormal">#000000</Color>
  <Color x:Key="ColorTypeFire">#F08030</Color>
  <Color x:Key="ColorOnTypeFire">#FFFFFF</Color>
  <Color x:Key="ColorTypeWater">#6890F0</Color>
  <Color x:Key="ColorOnTypeWater">#FFFFFF</Color>
  <Color x:Key="ColorTypeElectric">#F8D030</Color>
  <Color x:Key="ColorOnTypeElectric">#000000</Color>
  <Color x:Key="ColorTypeGrass">#78C850</Color>
  <Color x:Key="ColorOnTypeGrass">#FFFFFF</Color>
  <Color x:Key="ColorTypeIce">#98D8D8</Color>
  <Color x:Key="ColorOnTypeIce">#000000</Color>
  <Color x:Key="ColorTypeFighting">#C03028</Color>
  <Color x:Key="ColorOnTypeFighting">#FFFFFF</Color>
  <Color x:Key="ColorTypePoison">#A040A0</Color>
  <Color x:Key="ColorOnTypePoison">#FFFFFF</Color>
  <Color x:Key="ColorTypeGround">#E0C068</Color>
  <Color x:Key="ColorOnTypeGround">#000000</Color>
  <Color x:Key="ColorTypeFlying">#A890F0</Color>
  <Color x:Key="ColorOnTypeFlying">#FFFFFF</Color>
  <Color x:Key="ColorTypePsychic">#F85888</Color>
  <Color x:Key="ColorOnTypePsychic">#FFFFFF</Color>
  <Color x:Key="ColorTypeBug">#A8B820</Color>
  <Color x:Key="ColorOnTypeBug">#000000</Color>
  <Color x:Key="ColorTypeRock">#B8A038</Color>
  <Color x:Key="ColorOnTypeRock">#FFFFFF</Color>
  <Color x:Key="ColorTypeGhost">#705898</Color>
  <Color x:Key="ColorOnTypeGhost">#FFFFFF</Color>
  <Color x:Key="ColorTypeDragon">#7038F8</Color>
  <Color x:Key="ColorOnTypeDragon">#FFFFFF</Color>
  <Color x:Key="ColorTypeDark">#705848</Color>
  <Color x:Key="ColorOnTypeDark">#FFFFFF</Color>
  <Color x:Key="ColorTypeSteel">#B8B8D0</Color>
  <Color x:Key="ColorOnTypeSteel">#000000</Color>
  <Color x:Key="ColorTypeFairy">#EE99AC</Color>
  <Color x:Key="ColorOnTypeFairy">#000000</Color>
  <Color x:Key="ColorTypeStellar">#FFD700</Color>
  <Color x:Key="ColorOnTypeStellar">#000000</Color>

  <!-- Spacing -->
  <x:Double x:Key="Space0">0</x:Double>
  <x:Double x:Key="Space1">4</x:Double>
  <x:Double x:Key="Space2">8</x:Double>
  <x:Double x:Key="Space3">12</x:Double>
  <x:Double x:Key="Space4">16</x:Double>
  <x:Double x:Key="Space5">20</x:Double>
  <x:Double x:Key="Space6">24</x:Double>
  <x:Double x:Key="Space8">32</x:Double>
  <x:Double x:Key="Space10">40</x:Double>
  <x:Double x:Key="Space12">48</x:Double>
  <x:Double x:Key="Space16">64</x:Double>

  <!-- Border Radius -->
  <x:Double x:Key="RadiusNone">0</x:Double>
  <x:Double x:Key="RadiusSm">4</x:Double>
  <x:Double x:Key="RadiusMd">8</x:Double>
  <x:Double x:Key="RadiusLg">12</x:Double>
  <x:Double x:Key="RadiusXl">16</x:Double>
  <x:Double x:Key="RadiusFull">9999</x:Double>

  <!-- Shadows (as doubles for Thickness) -->
  <OnPlatform x:Key="ShadowSm" x:TypeArguments="Thickness">
    <On Platform="WinUI" Value="0,1,0,2" />
    <On Platform="iOS, Android, MacCatalyst" Value="0,1,0,2" />
  </OnPlatform>
  <OnPlatform x:Key="ShadowMd" x:TypeArguments="Thickness">
    <On Platform="WinUI" Value="0,4,0,8" />
    <On Platform="iOS, Android, MacCatalyst" Value="0,4,0,8" />
  </OnPlatform>
  <OnPlatform x:Key="ShadowLg" x:TypeArguments="Thickness">
    <On Platform="WinUI" Value="0,8,0,24" />
    <On Platform="iOS, Android, MacCatalyst" Value="0,8,0,24" />
  </OnPlatform>

  <!-- Typography -->
  <x:String x:Key="FontFamilyBase">Inter Variable</x:String>
  <x:String x:Key="FontFamilyMono">JetBrains Mono Variable</x:String>
  <x:String x:Key="FontFamilyDisplay">Press Start 2P</x:String>

  <x:Double x:Key="FontWeightThin">100</x:Double>
  <x:Double x:Key="FontWeightLight">300</x:Double>
  <x:Double x:Key="FontWeightRegular">400</x:Double>
  <x:Double x:Key="FontWeightMedium">500</x:Double>
  <x:Double x:Key="FontWeightSemibold">600</x:Double>
  <x:Double x:Key="FontWeightBold">700</x:Double>
  <x:Double x:Key="FontWeightExtrabold">800</x:Double>

  <x:Double x:Key="FontSizeDisplayXL">48</x:Double>
  <x:Double x:Key="FontSizeDisplayLG">36</x:Double>
  <x:Double x:Key="FontSizeDisplayMD">28</x:Double>
  <x:Double x:Key="FontSizeHeadingXL">24</x:Double>
  <x:Double x:Key="FontSizeHeadingLG">20</x:Double>
  <x:Double x:Key="FontSizeHeadingMD">18</x:Double>
  <x:Double x:Key="FontSizeHeadingSM">16</x:Double>
  <x:Double x:Key="FontSizeBodyLG">18</x:Double>
  <x:Double x:Key="FontSizeBodyMD">16</x:Double>
  <x:Double x:Key="FontSizeBodySM">14</x:Double>
  <x:Double x:Key="FontSizeCaption">12</x:Double>
  <x:Double x:Key="FontSizeOverline">12</x:Double>

  <x:Double x:Key="LineHeightTight">1.1</x:Double>
  <x:Double x:Key="LineHeightNormal">1.5</x:Double>
  <x:Double x:Key="LineHeightRelaxed">1.6</x:Double>

  <x:Double x:Key="LetterSpacingTight">-0.02</x:Double>
  <x:Double x:Key="LetterSpacingNormal">0</x:Double>
  <x:Double x:Key="LetterSpacingWide">0.01</x:Double>
  <x:Double x:Key="LetterSpacingWider">0.02</x:Double>
  <x:Double x:Key="LetterSpacingWidest">0.08</x:Double>

  <!-- Motion -->
  <x:Int32 x:Key="DurationInstant">0</x:Int32>
  <x:Int32 x:Key="DurationFast">100</x:Int32>
  <x:Int32 x:Key="DurationNormal">200</x:Int32>
  <x:Int32 x:Key="DurationSlow">300</x:Int32>
  <x:Int32 x:Key="DurationSlower">500</x:Int32>

  <!-- Z-Index -->
  <x:Int32 x:Key="ZBase">0</x:Int32>
  <x:Int32 x:Key="ZDropdown">100</x:Int32>
  <x:Int32 x:Key="ZSticky">200</x:Int32>
  <x:Int32 x:Key="ZOverlay">300</x:Int32>
  <x:Int32 x:Key="ZPopover">400</x:Int32>
  <x:Int32 x:Key="ZToast">500</x:Int32>
  <x:Int32 x:Key="ZTooltip">600</x:Int32>

  <!-- Opacity -->
  <x:Double x:Key="Opacity0">0</x:Double>
  <x:Double x:Key="Opacity10">0.1</x:Double>
  <x:Double x:Key="Opacity20">0.2</x:Double>
  <x:Double x:Key="Opacity30">0.3</x:Double>
  <x:Double x:Key="Opacity40">0.4</x:Double>
  <x:Double x:Key="Opacity50">0.5</x:Double>
  <x:Double x:Key="Opacity60">0.6</x:Double>
  <x:Double x:Key="Opacity70">0.7</x:Double>
  <x:Double x:Key="Opacity80">0.8</x:Double>
  <x:Double x:Key="Opacity90">0.9</x:Double>
  <x:Double x:Key="Opacity100">1</x:Double>
  <x:Double x:Key="OpacityDisabled">0.38</x:Double>
  <x:Double x:Key="OpacityHover">0.08</x:Double>
  <x:Double x:Key="OpacityPressed">0.12</x:Double>
  <x:Double x:Key="OpacityFocus">0.16</x:Double>
  <x:Double x:Key="OpacityDragged">0.5</x:Double>

</ResourceDictionary>
```

---

## Token Generation Script

### Node.js Build Script

```javascript
// scripts/generate-tokens.js
const fs = require('fs');
const path = require('path');

const tokens = require('./tokens.json'); // The JSON from above

function toCssVar(key, value) {
  return `  --${key}: ${value};`;
}

function flatten(obj, prefix = '') {
  let result = {};
  for (const [key, value] of Object.entries(obj)) {
    const newKey = prefix ? `${prefix}-${key}` : key;
    if (value && typeof value === 'object' && !value.value && !Array.isArray(value)) {
      Object.assign(result, flatten(value, newKey));
    } else if (value?.value !== undefined) {
      result[newKey] = value.value;
    } else if (typeof value === 'string' || typeof value === 'number') {
      result[newKey] = value;
    }
  }
  return result;
}

const flat = flatten(tokens);

// Generate CSS
const cssLines = [':root {'];
for (const [key, value] of Object.entries(flat)) {
  cssLines.push(toCssVar(key, value));
}
cssLines.push('}');
fs.writeFileSync(path.join(__dirname, '../wwwroot/css/tokens.css'), cssLines.join('\n'));

// Generate MAUI XAML (simplified)
const xamlLines = ['<ResourceDictionary xmlns="http://schemas.microsoft.com/dotnet/2021/maui"', 
  'xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"',
  'xmlns:sys="clr-namespace:System;assembly=netstandard">'];

for (const [key, value] of Object.entries(flat)) {
  const type = typeof value;
  const xamlKey = key.replace(/-/g, '');
  if (type === 'string' && value.startsWith('#')) {
    xamlLines.push(`  <Color x:Key="${xamlKey}">${value}</Color>`);
  } else if (type === 'number' || (type === 'string' && /^\d+(\.\d+)?$/.test(value))) {
    xamlLines.push(`  <x:Double x:Key="${xamlKey}">${value}</x:Double>`);
  } else if (type === 'string') {
    xamlLines.push(`  <x:String x:Key="${xamlKey}">${value}</x:String>`);
  }
}
xamlLines.push('</ResourceDictionary>');
fs.writeFileSync(path.join(__dirname, '../Resources/Styles/Tokens.xaml'), xamlLines.join('\n'));

console.log('Tokens generated!');
```

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | 2026-11-17 | Initial token definitions |

---

## Related Documents
- [BRAND.md](BRAND.md) — Brand identity
- [COLORS.md](COLORS.md) — Color palette & contrast details
- [TYPOGRAPHY.md](TYPOGRAPHY.md) — Typography scale details
- [COMPONENTS.md](COMPONENTS.md) — Component token usage
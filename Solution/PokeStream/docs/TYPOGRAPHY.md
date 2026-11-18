# PokéStream Collector - Typography Standards

> **Design Principle**: Clean, readable, gaming-adjacent without being "gimmicky." Inter Variable provides modern versatility; JetBrains Mono adds technical precision; Press Start 2P serves as rare display accent only.

---

## Font Families

### Primary: Inter Variable
**Source**: [Google Fonts](https://fonts.google.com/specimen/Inter) / [rsms.me/inter](https://rsms.me/inter/)
**License**: SIL Open Font License 1.1 (free commercial use)
**Weights**: 100–900 (variable), plus static fallbacks
**Features**: `cv02` (single-storey a), `cv03` (closed 4), `cv04` (dotted zero), `cv11` (compact punctuation)

```css
@font-face {
  font-family: 'Inter Variable';
  src: url('/fonts/InterVariable.woff2') format('woff2-variations');
  font-weight: 100 900;
  font-stretch: 75% 125%;
  font-display: swap;
  font-feature-settings: 'cv02', 'cv03', 'cv04', 'cv11', 'liga', 'clig', 'calt';
}

/* Fallback static weights for older browsers */
@font-face {
  font-family: 'Inter';
  src: url('/fonts/Inter-Regular.woff2') format('woff2');
  font-weight: 400;
  font-display: swap;
}
@font-face {
  font-family: 'Inter';
  src: url('/fonts/Inter-Medium.woff2') format('woff2');
  font-weight: 500;
  font-display: swap;
}
@font-face {
  font-family: 'Inter';
  src: url('/fonts/Inter-SemiBold.woff2') format('woff2');
  font-weight: 600;
  font-display: swap;
}
@font-face {
  font-family: 'Inter';
  src: url('/fonts/Inter-Bold.woff2') format('woff2');
  font-weight: 700;
  font-display: swap;
}
```

**CSS Stack**:
```css
--font-family-base: 'Inter Variable', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
```

### Monospace: JetBrains Mono Variable
**Source**: [JetBrains Mono](https://www.jetbrains.com/lp/mono/)
**License**: SIL Open Font License 1.1
**Weights**: 100–800 (variable)
**Features**: Ligatures for code (`!=` → `≠`, `==>` → `⇒`, etc.)

```css
@font-face {
  font-family: 'JetBrains Mono Variable';
  src: url('/fonts/JetBrainsMonoVariable.woff2') format('woff2-variations');
  font-weight: 100 800;
  font-display: swap;
  font-feature-settings: 'liga', 'clig', 'calt', 'zero', 'ss01', 'ss02', 'ss03', 'ss04', 'ss05', 'ss06', 'ss07', 'ss08';
}

--font-family-mono: 'JetBrains Mono Variable', 'JetBrains Mono', 'Fira Code', 'SF Mono', 'Monaco', 'Inconsolata', monospace;
```

### Display Accent: Press Start 2P
**Source**: [Google Fonts](https://fonts.google.com/specimen/Press+Start+2P)
**License**: SIL Open Font License 1.1
**Use ONLY for**: Logo wordmark, hero headlines, celebration moments, retro easter eggs
**Size**: Minimum 16px (pixel font, doesn't scale well)

```css
@font-face {
  font-family: 'Press Start 2P';
  src: url('/fonts/PressStart2P-Regular.woff2') format('woff2');
  font-weight: 400;
  font-display: swap;
}

--font-family-display: 'Press Start 2P', 'VT323', 'Courier New', monospace;
```

---

## Type Scale

### Base Configuration
- **Base font size**: 16px (1rem) — respects user browser settings
- **Line height base**: 1.6 (unitless, scales with font-size)
- **Scale ratio**: 1.25 (Major Third) — harmonious, readable steps

### Complete Token Table

| Token | Clamp (Min/Max) | Desktop | Tablet | Mobile | Weight | Line Height | Letter Spacing | Use Case |
|-------|-----------------|---------|--------|--------|--------|-------------|----------------|----------|
| `display-xl` | clamp(2.5rem, 5vw, 4rem) | 48px / 3rem | 42px / 2.625rem | 36px / 2.25rem | 700 | 1.1 | -0.02em | Hero, splash, major landing |
| `display-lg` | clamp(2rem, 4vw, 3rem) | 36px / 2.25rem | 32px / 2rem | 28px / 1.75rem | 700 | 1.15 | -0.01em | Page titles, section heroes |
| `display-md` | clamp(1.5rem, 3vw, 2.25rem) | 28px / 1.75rem | 26px / 1.625rem | 24px / 1.5rem | 600 | 1.2 | -0.01em | Section headers, card heroes |
| `heading-xl` | clamp(1.25rem, 2.5vw, 1.75rem) | 24px / 1.5rem | 22px / 1.375rem | 20px / 1.25rem | 600 | 1.3 | 0 | Card titles, modal headers |
| `heading-lg` | clamp(1.125rem, 2vw, 1.5rem) | 20px / 1.25rem | 19px / 1.1875rem | 18px / 1.125rem | 600 | 1.35 | 0 | Subsections, list headers |
| `heading-md` | clamp(1rem, 1.5vw, 1.25rem) | 18px / 1.125rem | 17px / 1.0625rem | 16px / 1rem | 500 | 1.4 | 0 | Small headers, sidebar titles |
| `heading-sm` | clamp(0.875rem, 1vw, 1rem) | 16px / 1rem | 15px / 0.9375rem | 14px / 0.875rem | 500 | 1.45 | 0.01em | Metadata labels, badges |
| `body-lg` | clamp(1rem, 1.5vw, 1.25rem) | 18px / 1.125rem | 17px / 1.0625rem | 16px / 1rem | 400 | 1.6 | 0 | Lead paragraphs, intros |
| `body-md` | clamp(0.875rem, 1vw, 1rem) | 16px / 1rem | 15px / 0.9375rem | 14px / 0.875rem | 400 | 1.6 | 0 | **Default body text** |
| `body-sm` | clamp(0.75rem, 0.8vw, 0.875rem) | 14px / 0.875rem | 13px / 0.8125rem | 12px / 0.75rem | 400 | 1.5 | 0.01em | Secondary text, descriptions |
| `caption` | clamp(0.625rem, 0.6vw, 0.75rem) | 12px / 0.75rem | 11px / 0.6875rem | 10px / 0.625rem | 400 | 1.4 | 0.02em | Timestamps, footnotes, chips |
| `overline` | clamp(0.625rem, 0.6vw, 0.75rem) | 12px / 0.75rem | 11px / 0.6875rem | 10px / 0.625rem | 500 | 1.4 | 0.08em | Category labels, all caps |
| `code-inline` | 0.875em | 14px | 13px | 12px | 400 | 1.5 | 0 | Inline code, commands |
| `code-block` | 0.875rem | 14px / 0.875rem | 13px / 0.8125rem | 12px / 0.75rem | 400 | 1.6 | 0 | Code blocks, logs |

### Fluid Type Implementation
```css
:root {
  /* Fluid type using clamp() — no media queries needed */
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
}
```

---

## Font Weight Scale

| Token | Weight | Numeric | Use Case |
|-------|--------|---------|----------|
| `weight-thin` | Thin | 100 | Rare, large display only |
| `weight-light` | Light | 300 | Large body, subtle emphasis |
| `weight-regular` | Regular | 400 | **Default body**, UI text |
| `weight-medium` | Medium | 500 | **Headings**, emphasis, buttons |
| `weight-semibold` | SemiBold | 600 | **Display headings**, strong CTAs |
| `weight-bold` | Bold | 700 | **Hero**, critical actions |
| `weight-extrabold` | ExtraBold | 800 | Rare, logo, extreme emphasis |

```css
:root {
  --weight-thin: 100;
  --weight-light: 300;
  --weight-regular: 400;
  --weight-medium: 500;
  --weight-semibold: 600;
  --weight-bold: 700;
  --weight-extrabold: 800;
}
```

---

## Line Height Scale

| Token | Value | Use Case |
|-------|-------|----------|
| `leading-none` | 1 | Icons, single-line buttons |
| `leading-tight` | 1.1 | Display headings |
| `leading-snug` | 1.25 | Large headings |
| `leading-normal` | 1.5 | Headings, subheads |
| `leading-relaxed` | 1.6 | **Body text (default)** |
| `leading-loose` | 1.75 | Long-form reading |
| `leading-code` | 1.6 | Code blocks |

```css
:root {
  --leading-none: 1;
  --leading-tight: 1.1;
  --leading-snug: 1.25;
  --leading-normal: 1.5;
  --leading-relaxed: 1.6;
  --leading-loose: 1.75;
  --leading-code: 1.6;
}
```

---

## Letter Spacing

| Token | Value | Use Case |
|-------|-------|----------|
| `tracking-tighter` | -0.02em | Display XL/LG |
| `tracking-tight` | -0.01em | Display MD, Heading XL |
| `tracking-normal` | 0 | Headings, body |
| `tracking-wide` | 0.01em | Body SM, captions |
| `tracking-wider` | 0.02em | Captions, overline |
| `tracking-widest` | 0.08em | **Overline (all caps)** |

```css
:root {
  --tracking-tighter: -0.02em;
  --tracking-tight: -0.01em;
  --tracking-normal: 0;
  --tracking-wide: 0.01em;
  --tracking-wider: 0.02em;
  --tracking-widest: 0.08em;
}
```

---

## Text Styles (Composable Classes)

### Display Styles
```css
.text-display-xl {
  font-family: var(--font-family-base);
  font-size: var(--text-display-xl);
  font-weight: var(--weight-bold);
  line-height: var(--leading-tight);
  letter-spacing: var(--tracking-tighter);
}

.text-display-lg {
  font-family: var(--font-family-base);
  font-size: var(--text-display-lg);
  font-weight: var(--weight-bold);
  line-height: var(--leading-tight);
  letter-spacing: var(--tracking-tight);
}

.text-display-md {
  font-family: var(--font-family-base);
  font-size: var(--text-display-md);
  font-weight: var(--weight-semibold);
  line-height: var(--leading-snug);
  letter-spacing: var(--tracking-tight);
}
```

### Heading Styles
```css
.text-heading-xl {
  font-family: var(--font-family-base);
  font-size: var(--text-heading-xl);
  font-weight: var(--weight-semibold);
  line-height: var(--leading-normal);
  letter-spacing: var(--tracking-normal);
}

.text-heading-lg {
  font-family: var(--font-family-base);
  font-size: var(--text-heading-lg);
  font-weight: var(--weight-semibold);
  line-height: var(--leading-normal);
  letter-spacing: var(--tracking-normal);
}

.text-heading-md {
  font-family: var(--font-family-base);
  font-size: var(--text-heading-md);
  font-weight: var(--weight-medium);
  line-height: var(--leading-normal);
  letter-spacing: var(--tracking-normal);
}

.text-heading-sm {
  font-family: var(--font-family-base);
  font-size: var(--text-heading-sm);
  font-weight: var(--weight-medium);
  line-height: var(--leading-normal);
  letter-spacing: var(--tracking-wide);
}
```

### Body Styles
```css
.text-body-lg {
  font-family: var(--font-family-base);
  font-size: var(--text-body-lg);
  font-weight: var(--weight-regular);
  line-height: var(--leading-relaxed);
  letter-spacing: var(--tracking-normal);
}

.text-body-md {
  font-family: var(--font-family-base);
  font-size: var(--text-body-md);
  font-weight: var(--weight-regular);
  line-height: var(--leading-relaxed);
  letter-spacing: var(--tracking-normal);
}

.text-body-sm {
  font-family: var(--font-family-base);
  font-size: var(--text-body-sm);
  font-weight: var(--weight-regular);
  line-height: var(--leading-relaxed);
  letter-spacing: var(--tracking-wide);
}
```

### Utility Styles
```css
.text-caption {
  font-family: var(--font-family-base);
  font-size: var(--text-caption);
  font-weight: var(--weight-regular);
  line-height: var(--leading-normal);
  letter-spacing: var(--tracking-wider);
  color: var(--color-on-surface-variant);
}

.text-overline {
  font-family: var(--font-family-base);
  font-size: var(--text-overline);
  font-weight: var(--weight-medium);
  line-height: var(--leading-normal);
  letter-spacing: var(--tracking-widest);
  text-transform: uppercase;
  color: var(--color-on-surface-variant);
}

.text-code-inline {
  font-family: var(--font-family-mono);
  font-size: var(--text-body-sm);
  font-weight: var(--weight-regular);
  line-height: var(--leading-code);
  letter-spacing: var(--tracking-normal);
  background: var(--color-surface-variant);
  padding: 0.125em 0.375em;
  border-radius: var(--radius-sm);
}

.text-code-block {
  font-family: var(--font-family-mono);
  font-size: var(--text-body-sm);
  font-weight: var(--weight-regular);
  line-height: var(--leading-code);
  letter-spacing: var(--tracking-normal);
  background: var(--color-surface-container);
  padding: var(--space-4);
  border-radius: var(--radius-md);
  overflow-x: auto;
  tab-size: 2;
}
```

---

## Responsive Behavior

### Breakpoints
```css
:root {
  --bp-sm: 640px;   /* Mobile landscape / small tablet */
  --bp-md: 768px;   /* Tablet portrait */
  --bp-lg: 1024px;  /* Tablet landscape / small desktop */
  --bp-xl: 1280px;  /* Desktop */
  --bp-2xl: 1536px; /* Large desktop */
}
```

### Fluid Scaling Strategy
- **Clamp-based**: All type tokens use `clamp(min, vw, max)` — no media queries for type
- **Container queries**: For components that need contextual sizing (cards in sidebar vs main)
- **Viewport units**: `vw` for display, `rem` for body (respects user zoom)

### Mobile-First Overrides (if needed)
```css
@media (max-width: 640px) {
  .text-display-xl { font-size: 2.5rem; }
  .text-display-lg { font-size: 2rem; }
  .text-display-md { font-size: 1.5rem; }
}
```

---

## Accessibility

### Minimum Requirements (WCAG 2.1 AA)
| Criterion | Requirement | Implementation |
|-----------|-------------|----------------|
| **Resize text** | 200% zoom without loss | Relative units (rem/clamp), no fixed px |
| **Line height** | ≥ 1.5 for paragraphs | `leading-relaxed` (1.6) default |
| **Letter spacing** | ≥ 0.12× font size | `tracking-wide`/`wider` for small text |
| **Word spacing** | ≥ 0.16× font size | Natural in Inter, no justification |
| **Contrast** | 4.5:1 normal, 3:1 large | Verified in [COLORS.md](COLORS.md) |
| **Font choice** | No italic/oblique for body | Only regular/medium weights for body |

### Dyslexia-Friendly Options
```css
/* Optional: dyslexia-friendly font override */
@media (prefers-reduced-motion: no-preference) {
  [data-font="dyslexic"] {
    --font-family-base: 'OpenDyslexic', 'Inter Variable', sans-serif;
  }
}
```

### High Contrast Mode
```css
@media (prefers-contrast: high) {
  :root {
    --font-weight-regular: 500;  /* Slightly bolder for contrast */
    --font-weight-medium: 600;
    --font-weight-semibold: 700;
  }
}
```

### Reduced Motion
```css
@media (prefers-reduced-motion: reduce) {
  /* Disable font loading animations */
  @font-face {
    font-display: block; /* No swap flash */
  }
}
```

---

## Platform-Specific Notes

### Blazor WASM (Web App)
- **Self-host fonts**: Bundle WOFF2 in `wwwroot/fonts/`
- **Preload critical**: `<link rel="preload" as="font" href="/fonts/InterVariable.woff2" crossorigin>`
- **Font display**: `swap` for body, `block` for display (prevents layout shift)
- **Subsetting**: Latin + Latin Extended only (drop Cyrillic/Greek if not needed)

### MAUI Overlay (Desktop)
- **System fonts preferred**: Use `-apple-system`, `Segoe UI` for overlay to match OS
- **Inter as fallback**: Bundle for consistency if system font unavailable
- **Scaling**: Respect Windows DPI scaling (MAUI handles automatically)
- **ClearType**: Ensure `font-smoothing: antialiased` on Windows

### Bots (Chat)
- **Monospace only**: JetBrains Mono for code/commands in chat
- **No custom fonts**: Chat platforms don't support custom fonts
- **Formatting**: Use platform markdown (Twitch: `**bold**`, `*italic*`, `` `code` ``)

---

## CSS Custom Properties Summary

```css
:root {
  /* Font Families */
  --font-family-base: 'Inter Variable', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
  --font-family-mono: 'JetBrains Mono Variable', 'JetBrains Mono', 'Fira Code', monospace;
  --font-family-display: 'Press Start 2P', 'VT323', monospace;

  /* Font Weights */
  --weight-thin: 100;
  --weight-light: 300;
  --weight-regular: 400;
  --weight-medium: 500;
  --weight-semibold: 600;
  --weight-bold: 700;
  --weight-extrabold: 800;

  /* Line Heights */
  --leading-none: 1;
  --leading-tight: 1.1;
  --leading-snug: 1.25;
  --leading-normal: 1.5;
  --leading-relaxed: 1.6;
  --leading-loose: 1.75;
  --leading-code: 1.6;

  /* Letter Spacing */
  --tracking-tighter: -0.02em;
  --tracking-tight: -0.01em;
  --tracking-normal: 0;
  --tracking-wide: 0.01em;
  --tracking-wider: 0.02em;
  --tracking-widest: 0.08em;

  /* Fluid Type Scale */
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
}
```

---

## MAUI ResourceDictionary

```xml
<!-- Fonts.xaml -->
<ResourceDictionary>
  <!-- Font Families -->
  <OnPlatform x:Key="FontFamilyBase" x:TypeArguments="x:String">
    <On Platform="iOS, MacCatalyst" Value="Inter Variable, San Francisco" />
    <On Platform="Android" Value="Inter Variable, Roboto" />
    <On Platform="WinUI" Value="Inter Variable, Segoe UI" />
    <On Platform="Tizen" Value="Inter Variable, Tizen Sans" />
  </OnPlatform>

  <OnPlatform x:Key="FontFamilyMono" x:TypeArguments="x:String">
    <On Platform="iOS, MacCatalyst" Value="JetBrains Mono Variable, Menlo" />
    <On Platform="Android" Value="JetBrains Mono Variable, Roboto Mono" />
    <On Platform="WinUI" Value="JetBrains Mono Variable, Cascadia Mono" />
  </OnPlatform>

  <x:String x:Key="FontFamilyDisplay">Press Start 2P</x:String>

  <!-- Font Sizes (using device-independent units) -->
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

  <!-- Font Weights -->
  <x:Double x:Key="FontWeightThin">100</x:Double>
  <x:Double x:Key="FontWeightLight">300</x:Double>
  <x:Double x:Key="FontWeightRegular">400</x:Double>
  <x:Double x:Key="FontWeightMedium">500</x:Double>
  <x:Double x:Key="FontWeightSemibold">600</x:Double>
  <x:Double x:Key="FontWeightBold">700</x:Double>

  <!-- Line Heights (multipliers) -->
  <x:Double x:Key="LineHeightTight">1.1</x:Double>
  <x:Double x:Key="LineHeightNormal">1.5</x:Double>
  <x:Double x:Key="LineHeightRelaxed">1.6</x:Double>

  <!-- Letter Spacing -->
  <x:Double x:Key="LetterSpacingTight">-0.02</x:Double>
  <x:Double x:Key="LetterSpacingNormal">0</x:Double>
  <x:Double x:Key="LetterSpacingWide">0.01</x:Double>
  <x:Double x:Key="LetterSpacingWider">0.02</x:Double>
  <x:Double x:Key="LetterSpacingWidest">0.08</x:Double>
</ResourceDictionary>
```

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | 2026-11-17 | Initial typography standards |

---

## Related Documents
- [BRAND.md](BRAND.md) — Brand identity
- [COLORS.md](COLORS.md) — Color palette & contrast
- [DESIGN-TOKENS.md](DESIGN-TOKENS.md) — Complete token reference
- [COMPONENTS.md](COMPONENTS.md) — Component typography usage
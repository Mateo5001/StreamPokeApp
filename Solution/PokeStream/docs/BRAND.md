# PokéStream Collector - Brand Guidelines

## Brand Identity

### Name
**PokéStream Collector** (full) / **PokéStream** (short)

### Tagline
> *Catch 'em all on stream*

### Brand Personality
- **Playful** — Gaming spirit, collecting joy
- **Energetic** — Live streaming energy, real-time interaction
- **Nostalgic** — Pokémon heritage, familiar yet fresh
- **Accessible** — Cross-platform, inclusive community
- **Modern** — Clean tech, real-time sync, PWA ready

### Brand Voice
| Context | Tone | Example |
|---------|------|---------|
| UI Copy | Friendly, encouraging | "Your Bulbasaur is ready to battle!" |
| Error States | Helpful, not technical | "Oops! That Pokémon got away. Try again?" |
| Onboarding | Exciting, guiding | "Welcome Trainer! Let's start your collection." |
| Notifications | Urgent but fun | "⚡ Rare spawn! A Pikachu appeared!" |
| Settings | Clear, respectful | "Manage your privacy preferences" |

---

## Logo Usage

### Primary Logo
```
[PokéBall Icon] PokéStream Collector
```
- **Icon**: Stylized PokéBall with streaming "signal" waves
- **Wordmark**: Custom rounded sans-serif, "PokéStream" bold, "Collector" regular
- **Clear space**: Minimum 1x icon height on all sides

### Logo Variants
| Variant | Use Case | Background |
|---------|----------|------------|
| Full color | Primary, light backgrounds | White/light |
| Monochrome white | Dark overlays, stream overlay | Dark/transparent |
| Monochrome dark | Light print, docs | White/light |
| Icon only | App icon, favicon, mobile | Any (with safe area) |

### Minimum Sizes
- **Full logo**: 120px width (digital), 25mm (print)
- **Icon only**: 32px × 32px (digital), 10mm (print)

### Don'ts
- ❌ Don't rotate, stretch, or skew
- ❌ Don't change colors (use approved variants)
- ❌ Don't add effects (drop shadow, glow, outline)
- ❌ Don't place on busy backgrounds without safe area
- ❌ Don't separate icon from wordmark in primary usage

---

## Color System

### Philosophy
Colors inspired by **Pokémon types** but refined for **streaming UI** — vibrant enough for overlay visibility, accessible for WCAG AA compliance.

See [COLORS.md](COLORS.md) for complete palette, contrast ratios, and usage rules.

### Quick Reference
| Role | Color | Hex | Usage |
|------|-------|-----|-------|
| Primary | Electric Yellow | `#FFD700` | CTAs, highlights, active states |
| Secondary | PokéBall Red | `#E53E3E` | Accents, alerts, capture button |
| Background | Deep Night | `#0D1117` | Overlay base, dark mode default |
| Surface | Charcoal | `#161B22` | Cards, panels, modals |
| Text Primary | Pure White | `#FFFFFF` | Primary content |
| Text Muted | Muted Gray | `#8B949E` | Secondary info, placeholders |
| Success | Grass Green | `#3FB950` | Captured, verified, online |
| Warning | Fire Orange | `#F78166` | Cooldowns, pending |
| Error | Fair Red | `#F85149` | Failed, disconnected |
| Info | Water Blue | `#58A6FF` | Links, info, water-type |

---

## Typography

### Font Stack
```css
/* Primary - UI, Headings, Brand */
font-family: 'Inter Variable', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;

/* Monospace - Codes, IDs, Technical */
font-family: 'JetBrains Mono Variable', 'JetBrains Mono', 'Fira Code', monospace;

/* Display - Logo, Hero (optional) */
font-family: 'Press Start 2P', cursive; /* Retro gaming accent only */
```

See [TYPOGRAPHY.md](TYPOGRAPHY.md) for complete scale, weights, line heights, and responsive rules.

### Type Scale (Base: 16px)
| Token | Size | Weight | Line Height | Use Case |
|-------|------|--------|-------------|----------|
| `display-xl` | 48px / 3rem | 700 | 1.1 | Hero, splash screen |
| `display-lg` | 36px / 2.25rem | 700 | 1.15 | Page titles |
| `display-md` | 28px / 1.75rem | 600 | 1.2 | Section headers |
| `heading-xl` | 24px / 1.5rem | 600 | 1.3 | Card titles, modals |
| `heading-lg` | 20px / 1.25rem | 600 | 1.35 | Subsections |
| `heading-md` | 18px / 1.125rem | 500 | 1.4 | List headers |
| `heading-sm` | 16px / 1rem | 500 | 1.45 | Small headers |
| `body-lg` | 18px / 1.125rem | 400 | 1.6 | Lead paragraphs |
| `body-md` | 16px / 1rem | 400 | 1.6 | Default body text |
| `body-sm` | 14px / 0.875rem | 400 | 1.5 | Secondary text |
| `caption` | 12px / 0.75rem | 400 | 1.4 | Labels, timestamps |
| `code` | 14px / 0.875rem | 400 | 1.5 | Inline code, commands |

---

## Iconography

### Style
- **Weight**: 2px stroke (outline), 1.5px (small)
- **Corner radius**: 2px (consistent with UI)
- **Style**: Rounded, friendly, slightly geometric
- **Grid**: 24×24px base, optical alignment

### Sources
- **Primary**: Phosphor Icons (duotone for brand, regular for UI)
- **Pokémon sprites**: Official assets (fair use for display), custom for UI
- **Platform logos**: Official Twitch/YouTube/Kick brand assets

### Categories
| Category | Examples | Style Notes |
|----------|----------|-------------|
| Navigation | Home, Collection, Shop, Settings | Outline, 24px |
| Actions | Capture, Trade, Evolve, Release | Filled on active |
| Status | Online, Offline, Verified, Pending | Color-coded dots |
| Pokémon Types | 18 type badges | Filled, rounded corners |
| Platform | Twitch, YouTube, Kick, Discord | Brand colors, official shapes |

---

## Spacing System

### Base Unit: 4px
All spacing values are multiples of 4px.

| Token | Value | Rem | Use Case |
|-------|-------|-----|----------|
| `space-0` | 0 | 0 | Reset |
| `space-1` | 4px | 0.25rem | Tight gaps |
| `space-2` | 8px | 0.5rem | Default gap |
| `space-3` | 12px | 0.75rem | Component padding |
| `space-4` | 16px | 1rem | Standard padding |
| `space-5` | 20px | 1.25rem | Generous padding |
| `space-6` | 24px | 1.5rem | Section spacing |
| `space-8` | 32px | 2rem | Large sections |
| `space-10` | 40px | 2.5rem | Page margins |
| `space-12` | 48px | 3rem | Hero spacing |
| `space-16` | 64px | 4rem | Major breaks |

---

## Border Radius

| Token | Value | Use Case |
|-------|-------|----------|
| `radius-none` | 0 | Sharp edges (tables, code) |
| `radius-sm` | 4px | Badges, tags, small chips |
| `radius-md` | 8px | Buttons, inputs, cards |
| `radius-lg` | 12px | Modals, panels, dropdowns |
| `radius-xl` | 16px | Large containers, sheets |
| `radius-full` | 9999px | Pills, avatars, progress rings |

---

## Shadows & Elevation

### Shadow Scale (Dark Mode Optimized)
| Level | Token | Box Shadow | Use Case |
|-------|-------|------------|----------|
| 0 | `shadow-none` | none | Flat, inline |
| 1 | `shadow-sm` | `0 1px 2px rgba(0,0,0,0.3)` | Cards, default |
| 2 | `shadow-md` | `0 4px 8px rgba(0,0,0,0.4)` | Raised cards, dropdowns |
| 3 | `shadow-lg` | `0 8px 24px rgba(0,0,0,0.5)` | Modals, sheets |
| 4 | `shadow-xl` | `0 16px 48px rgba(0,0,0,0.6)` | Toasts, popovers |
| Glow | `shadow-glow` | `0 0 24px rgba(255,215,0,0.3)` | Primary CTAs, rare spawns |

---

## Motion & Animation

### Principles
- **Purposeful**: Every animation communicates state change
- **Performant**: 60fps, GPU-accelerated (transform/opacity only)
- **Respectful**: Honor `prefers-reduced-motion`
- **Branded**: Spring physics (stiffness 300, damping 30) for playful feel

### Duration Tokens
| Token | Duration | Use Case |
|-------|----------|----------|
| `duration-instant` | 0ms | Immediate feedback |
| `duration-fast` | 100ms | Hover, focus, tap |
| `duration-normal` | 200ms | Transitions, toggles |
| `duration-slow` | 300ms | Modals, drawers, complex |
| `duration-slower` | 500ms | Page transitions, celebrations |

### Easing
| Token | Curve | Use Case |
|-------|-------|----------|
| `ease-standard` | `cubic-bezier(0.2, 0, 0.38, 0.9)` | Default, Material-like |
| `ease-spring` | `cubic-bezier(0.34, 1.56, 0.64, 1)` | Playful entrances |
| `ease-sharp` | `cubic-bezier(0.4, 0, 1, 1)` | Exits, dismissals |
| `ease-elastic` | Custom spring | Rare spawn, catch animation |

---

## Accessibility Standards

### Contrast Requirements (WCAG 2.1 AA)
| Element | Minimum Ratio | Target Ratio |
|---------|---------------|--------------|
| Normal text | 4.5:1 | 7:1 (AAA) |
| Large text (18px+ bold, 24px+) | 3:1 | 4.5:1 |
| UI components (borders, icons) | 3:1 | 4.5:1 |
| Focus indicators | 3:1 | 4.5:1 |

### Focus Management
- **Visible focus**: 2px solid `color-primary` + 2px offset `color-background`
- **Focus order**: Logical, matches visual hierarchy
- **Skip links**: Provided for main navigation
- **Trapped focus**: Modals, drawers, toasts

### Screen Readers
- Semantic HTML5 elements
- ARIA labels for icon-only buttons
- Live regions for dynamic updates (spawns, captures)
- Alt text for all informative images

---

## Platform Adaptations

### MAUI Overlay (Streamer Desktop)
- **Always-on-top**, transparent background
- **Minimal chrome** — only Pokémon, capture indicators
- **High contrast mode** toggle for stream visibility
- **Reduced motion** by default (streamer preference)
- **Color blind safe** palette option

### Blazor WASM (Viewer Web App)
- **PWA** — installable, offline-capable
- **Responsive** — mobile-first, desktop-enhanced
- **Theme**: System preference + manual toggle
- **Full brand expression** — colors, motion, illustrations

### Bots (Twitch/YouTube/Kick)
- **Text-only** branding in chat
- **Emotes/badges** for subscribers
- **Consistent command syntax** across platforms

---

## Asset Library

### Location
```
/docs/assets/
├── logos/
│   ├── pokestream-full.svg
│   ├── pokestream-icon.svg
│   ├── pokestream-mono-white.svg
│   └── pokestream-mono-dark.svg
├── icons/
│   ├── phosphor/ (duotone, regular, fill, light, thin)
│   └── pokemon-types/ (18 SVG badges)
├── illustrations/
│   ├── empty-state-collection.svg
│   ├── empty-state-search.svg
│   ├── onboarding-1.svg
│   ├── onboarding-2.svg
│   └── celebration-catch.svg
└── platform/
    ├── twitch-badge.svg
    ├── youtube-badge.svg
    ├── kick-badge.svg
    └── discord-badge.svg
```

### Export Formats
- **SVG** — Source of truth, scalable
- **PNG** — @1x, @2x, @3x for raster fallbacks
- **WebP** — Optimized web delivery
- **AVIF** — Modern browsers, smallest size

---

## Version History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0.0 | 2026-11-17 | Claude | Initial brand guidelines |

---

## Related Documents
- [COLORS.md](COLORS.md) — Complete color palette & contrast tables
- [TYPOGRAPHY.md](TYPOGRAPHY.md) — Type scale, fonts, responsive rules
- [DESIGN-TOKENS.md](DESIGN-TOKENS.md) — Design token reference (JSON/CSS)
- [COMPONENTS.md](COMPONENTS.md) — Component library guidelines
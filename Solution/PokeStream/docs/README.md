# PokéStream Collector - Design Documentation

> **Single source of truth** for brand, design system, and component library.

---

## Documentation Index

| Document | Description | Audience |
|----------|-------------|----------|
| **[BRAND.md](BRAND.md)** | Brand identity, logo usage, voice, personality | Design, Marketing, PM |
| **[COLORS.md](COLORS.md)** | Complete color palette, contrast ratios, semantic roles | Design, Frontend, QA |
| **[TYPOGRAPHY.md](TYPOGRAPHY.md)** | Font families, type scale, line heights, accessibility | Design, Frontend |
| **[DESIGN-TOKENS.md](DESIGN-TOKENS.md)** | Machine-readable tokens (JSON, CSS, MAUI XAML) | Frontend, MAUI, Tooling |
| **[COMPONENTS.md](COMPONENTS.md)** | Component specs, APIs, states, accessibility | Frontend, MAUI, QA |

---

## Quick Start

### For Web (Blazor)
```bash
# 1. Copy tokens to wwwroot
cp docs/tokens.json wwwroot/css/tokens.json

# 2. Generate CSS variables (one-time setup)
node scripts/generate-tokens.js

# 3. Import in global.css
@import './css/tokens.css';
```

### For MAUI
```bash
# 1. Copy tokens to Resources
cp docs/tokens.json Resources/Styles/tokens.json

# 2. Generate XAML (one-time setup)
node scripts/generate-tokens.js

# 3. Merge in App.xaml
<ResourceDictionary Source="Styles/Tokens.xaml" />
```

---

## Design Principles

1. **Token-First** — No hardcoded values; everything references tokens
2. **Accessibility by Default** — WCAG 2.1 AA minimum, AAA target
3. **Dark Mode First** — Overlay runs in dark; light mode supported
4. **Platform Adaptive** — Web gets full expression; Overlay gets streamlined
5. **Performance Conscious** — Fluid type, GPU animations, minimal bundles

---

## Token Categories

| Category | Count | Description |
|----------|-------|-------------|
| **Color** | 60+ | Semantic, platform, rarity, type, gradients |
| **Spacing** | 11 | 4px base unit scale |
| **Border Radius** | 6 | 0 → full |
| **Shadow** | 7 | Elevation + glow |
| **Typography** | 30+ | Families, weights, sizes, leading, tracking |
| **Motion** | 9 | Durations, easings |
| **Z-Index** | 8 | Layering system |
| **Breakpoints** | 5 | Container query ready |
| **Opacity** | 16 | State overlays |

---

## Component Status

| Component | Web | MAUI | Tests | A11y | Docs |
|-----------|-----|------|-------|------|------|
| Button | 🟢 | 🟢 | ⬜ | ⬜ | ✅ |
| Badge | 🟢 | 🟢 | ⬜ | ⬜ | ✅ |
| Card | 🟢 | 🟢 | ⬜ | ⬜ | ✅ |
| PokemonCard | 🟢 | 🟢 | ⬜ | ⬜ | ✅ |
| CaptureButton | 🟢 | 🟢 | ⬜ | ⬜ | ✅ |
| SpawnNotification | 🟢 | 🟢 | ⬜ | ⬜ | ✅ |
| Input | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| Modal | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| ... | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |

---

## Contrast Quick Reference

| Combination | Dark Mode | Light Mode | Status |
|-------------|-----------|------------|--------|
| Primary Button | 8.2:1 ✅ | 5.8:1 ✅ | AA/AAA |
| Secondary Button | 5.3:1 ✅ | 5.3:1 ✅ | AA |
| Error Button | 4.9:1 ✅ | 5.3:1 ✅ | AA |
| Input Border | 4.1:1 ⚠️ | 2.9:1 ❌ | Fix needed light |
| Card Border | 4.1:1 ⚠️ | 3.2:1 ❌ | Fix needed light |
| Body Text | 12.6:1 ✅ | 15.2:1 ✅ | AAA |

> See [COLORS.md](COLORS.md#contrast-ratio-tables) for full tables.

---

## File Structure

```
docs/
├── README.md           # This file
├── BRAND.md           # Brand guidelines
├── COLORS.md          # Color palette & contrast
├── TYPOGRAPHY.md      # Typography standards
├── DESIGN-TOKENS.md   # Machine-readable tokens
├── COMPONENTS.md      # Component library
├── assets/            # Logos, icons, illustrations
│   ├── logos/
│   ├── icons/
│   ├── illustrations/
│   └── platform/
└── scripts/
    └── generate-tokens.js  # Token build script
```

---

## Contributing

### Adding New Tokens
1. Edit `DESIGN-TOKENS.md` JSON section
2. Run `node scripts/generate-tokens.js`
3. Verify CSS/XAML output
4. Update component docs if needed

### Adding New Components
1. Add to [COMPONENTS.md](COMPONENTS.md) inventory table
2. Write specification following existing patterns
3. Include: Props, States, CSS, Accessibility, Tests
4. Add to status table in this README

### Updating Colors
1. Edit `COLORS.md` palette tables
2. Update `DESIGN-TOKENS.md` color section
3. Run token generation
4. Verify contrast in [COLORS.md](COLORS.md#contrast-ratio-tables)
5. Update any affected component specs

---

## Tools & Validation

| Tool | Purpose | Command |
|------|---------|---------|
| **axe DevTools** | Automated a11y scan | Browser extension |
| **WAVE** | Visual contrast overlay | wave.webaim.org |
| **Color Contrast Analyzer** | Desktop contrast check | TPGi CCA |
| **Storybook a11y** | Component-level a11y | `npm run storybook` |
| **Chromatic** | Visual regression | `npx chromatic` |

---

## Version History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0.0 | 2026-11-17 | Claude | Initial design documentation |

---

## Related Links

- [PokéStream API Docs](../PokeStream.API/swagger) — Backend API reference
- [CLAUDE.md](../CLAUDE.md) — Project context & commands
- [Figma Design System](#) — Design file (when created)
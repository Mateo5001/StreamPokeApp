# PokéStream Collector - Component Library Guidelines

> **Design Principle**: Consistent, accessible, performant components built on design tokens. Each component follows atomic design: Atoms → Molecules → Organisms → Templates → Pages.

---

## Component Inventory

### Atoms (Foundation)
| Component | Status | Platforms | Description |
|-----------|--------|-----------|-------------|
| `Button` | 🟢 Planned | Blazor, MAUI | Primary, secondary, ghost, icon, capture |
| `Icon` | 🟢 Planned | Blazor, MAUI | Phosphor icons + Pokémon type icons |
| `Badge` | 🟢 Planned | Blazor, MAUI | Rarity, type, status, platform |
| `Typography` | 🟢 Planned | Blazor, MAUI | Heading, body, caption, code, overline |
| `Input` | 🟢 Planned | Blazor, MAUI | Text, search, number, password |
| `Checkbox` | 🟢 Planned | Blazor, MAUI | Single, indeterminate |
| `Radio` | 🟢 Planned | Blazor, MAUI | Radio group |
| `Switch` | 🟢 Planned | Blazor, MAUI | Toggle on/off |
| `Avatar` | 🟢 Planned | Blazor, MAUI | User, Pokémon, platform |
| `Divider` | 🟢 Planned | Blazor, MAUI | Horizontal, vertical |
| `Spinner` | 🟢 Planned | Blazor, MAUI | Loading, skeleton |
| `Tooltip` | 🟢 Planned | Blazor, MAUI | Hover/focus info |

### Molecules (Combinations)
| Component | Status | Platforms | Description |
|-----------|--------|-----------|-------------|
| `Card` | 🟢 Planned | Blazor, MAUI | Pokémon card, user card, item card |
| `CardStack` | 🟢 Planned | Blazor, MAUI | Stacked collection cards |
| `SearchInput` | 🟢 Planned | Blazor, MAUI | Search with filters, suggestions |
| `FilterChips` | 🟢 Planned | Blazor, MAUI | Type, rarity, generation filters |
| `StatBar` | 🟢 Planned | Blazor, MAUI | HP, Attack, Defense, etc. |
| `ProgressRing` | 🟢 Planned | Blazor, MAUI | Capture progress, XP, cooldown |
| `Dropdown` | 🟢 Planned | Blazor, MAUI | Select, multi-select |
| `Tabs` | 🟢 Planned | Blazor, MAUI | Navigation, content switching |
| `Alert` | 🟢 Planned | Blazor, MAUI | Toast, inline, banner |
| `Modal` | 🟢 Planned | Blazor, MAUI | Confirm, form, info |
| `Drawer` | 🟢 Planned | Blazor, MAUI | Sidebar, filter panel |
| `Pagination` | 🟢 Planned | Blazor, MAUI | Page numbers, infinite scroll |
| `Breadcrumb` | 🟢 Planned | Blazor, MAUI | Navigation trail |

### Organisms (Complex)
| Component | Status | Platforms | Description |
|-----------|--------|-----------|-------------|
| `PokemonCard` | 🟢 Planned | Blazor, MAUI | Full Pokémon display with stats |
| `PokemonGrid` | 🟢 Planned | Blazor, MAUI | Responsive collection grid |
| `CaptureButton` | 🟢 Planned | Blazor, MAUI | Animated capture with cooldown |
| `SpawnNotification` | 🟢 Planned | Blazor, MAUI | Rare spawn alert with animation |
| `CollectionHeader` | 🟢 Planned | Blazor, MAUI | Stats, filters, search |
| `UserProfile` | 🟢 Planned | Blazor, MAUI | Avatar, stats, badges |
| `PlatformConnect` | 🟢 Planned | Blazor, MAUI | OAuth buttons, status |
| `Leaderboard` | 🟢 Planned | Blazor, MAUI | Rankings, pagination |
| `TradePanel` | 🟢 Planned | Blazor, MAUI | Offer, request, confirm |
| `EvolutionPreview` | 🟢 Planned | Blazor, MAUI | Before/after evolution |

---

## Component API Standards

### Naming Convention
```
PascalCase for components: <PokemonCard />
camelCase for parameters: @bind-Value, OnClick
kebab-case for CSS classes: .pokemon-card__image
BEM for modifiers: .pokemon-card--rare, .pokemon-card__name--shiny
```

### Parameter Patterns
```razor
<!-- Standard parameter structure -->
<PokemonCard 
    Pokemon="@pokemon"           -- Required data
    Variant="CardVariant.Full"   -- Enum variant
    Size="ComponentSize.Medium"  -- Enum size
    ShowActions="true"           -- Boolean feature flags
    OnCapture="HandleCapture"    -- EventCallback<EventArgs>
    OnClick="NavigateToDetail"   -- EventCallback<MouseEventArgs>
    Class="custom-class"         -- Additional CSS classes
    Style="custom-style"         -- Inline styles (avoid)
/>
```

### EventCallback Naming
| Action | Pattern | Payload |
|--------|---------|---------|
| Click | `OnClick` | `MouseEventArgs` |
| Change | `OnChange` | `TValue` |
| Submit | `OnSubmit` | `TModel` |
| Capture | `OnCapture` | `PokemonCaptureArgs` |
| Close | `OnClose` | `EventArgs` |
| Select | `OnSelect` | `TItem` |

---

## Core Components Specification

### Button

#### Variants
| Variant | Use Case | Colors (Dark) | Colors (Light) |
|---------|----------|---------------|----------------|
| `Primary` | Main CTAs | `bg: primary`, `text: on-primary` | `bg: primary`, `text: on-primary` |
| `Secondary` | Capture, destructive | `bg: secondary`, `text: on-secondary` | `bg: secondary`, `text: on-secondary` |
| `Tertiary` | Success, positive | `bg: tertiary`, `text: on-tertiary` | `bg: tertiary`, `text: on-tertiary` |
| `Outline` | Secondary actions | `border: outline`, `text: primary`, `bg: transparent` | `border: outline`, `text: primary`, `bg: transparent` |
| `Ghost` | Subtle actions | `text: on-surface`, `bg: transparent` | `text: on-surface`, `bg: transparent` |
| `Capture` | **Special** - Animated capture | `gradient: capture`, `text: on-secondary` | `gradient: capture`, `text: on-secondary` |

#### Sizes
| Size | Padding | Font Size | Height | Use Case |
|------|---------|-----------|--------|----------|
| `Small` | `space-2 space-3` | `body-sm` | 32px | Dense UIs, icon buttons |
| `Medium` | `space-3 space-4` | `body-md` | 40px | **Default** |
| `Large` | `space-4 space-6` | `body-lg` | 48px | Hero CTAs, capture button |

#### States
| State | Visual | CSS |
|-------|--------|-----|
| Default | Base variant | `.btn` |
| Hover | `hover` color / opacity overlay | `.btn:hover` |
| Active/Pressed | Scale 0.98 + pressed color | `.btn:active` |
| Focus | 2px `outline` ring + offset | `.btn:focus-visible` |
| Disabled | `opacity-disabled` (0.38) | `.btn:disabled` |
| Loading | Spinner + disabled | `.btn--loading` |

#### Blazor Implementation
```razor
<!-- Button.razor -->
<button class="@ClassNames" 
        type="@Type" 
        disabled="@Disabled" 
        @onclick="OnClick"
        @onclick:preventDefault="true">
    @if (Loading) {
        <Spinner Size="SpinnerSize.Small" Class="btn__spinner" />
    } else if (Icon != null) {
        <Icon Name="@Icon" Class="btn__icon" />
    }
    <span class="btn__text">@ChildContent</span>
</button>

@code {
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;
    [Parameter] public string Icon { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public string Class { get; set; }
    
    private string ClassNames => new ClassBuilder()
        .Add("btn")
        .Add($"btn--{Variant.ToString().ToLower()}")
        .Add($"btn--{Size.ToString().ToLower()}")
        .Add("btn--loading", Loading)
        .Add("btn--disabled", Disabled)
        .Add(Class)
        .Build();
}
```

#### CSS (Token-based)
```css
.btn {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: var(--space-2);
  font-family: var(--font-family-base);
  font-weight: var(--weight-medium);
  border: none;
  border-radius: var(--radius-md);
  cursor: pointer;
  transition: all var(--duration-fast) var(--ease-standard);
  white-space: nowrap;
  text-decoration: none;
}

.btn:focus-visible {
  outline: 2px solid var(--color-primary);
  outline-offset: 2px;
}

.btn:disabled,
.btn--disabled {
  opacity: var(--opacity-disabled);
  cursor: not-allowed;
}

/* Variants */
.btn--primary {
  background: var(--color-primary);
  color: var(--color-on-primary);
}
.btn--primary:hover:not(:disabled) {
  background: var(--color-primary-hover);
}
.btn--primary:active:not(:disabled) {
  background: var(--color-primary-container);
}

.btn--secondary {
  background: var(--color-secondary);
  color: var(--color-on-secondary);
}
.btn--secondary:hover:not(:disabled) {
  background: var(--color-secondary-hover);
}

.btn--capture {
  background: var(--gradient-capture);
  color: var(--color-on-secondary);
  box-shadow: var(--shadow-glow);
}
.btn--capture:hover:not(:disabled) {
  box-shadow: var(--shadow-glow-strong);
  transform: translateY(-1px);
}
.btn--capture:active:not(:disabled) {
  transform: scale(0.98);
}

/* Sizes */
.btn--small {
  padding: var(--space-2) var(--space-3);
  font-size: var(--text-body-sm);
  height: 32px;
}
.btn--medium {
  padding: var(--space-3) var(--space-4);
  font-size: var(--text-body-md);
  height: 40px;
}
.btn--large {
  padding: var(--space-4) var(--space-6);
  font-size: var(--text-body-lg);
  height: 48px;
}
```

---

### Badge

#### Variants
| Variant | Token | Use Case |
|---------|-------|----------|
| `Rarity` | `color-rarity-*` | Pokémon rarity tier |
| `Type` | `color-type-*` | Pokémon type (18 types) |
| `Status` | `color-tertiary`/`warning`/`error`/`info` | Online, pending, verified |
| `Platform` | `color-platform-*` | Twitch, YouTube, Kick, Discord |
| `Count` | `color-primary-container` | Notification counts |
| `New` | `color-tertiary` | "NEW" indicator |

#### Sizes
| Size | Padding | Font Size | Border Radius | Use Case |
|------|---------|-----------|---------------|----------|
| `Small` | `space-1 space-2` | `caption` | `radius-sm` | Inline, tight spaces |
| `Medium` | `space-1 space-3` | `body-sm` | `radius-md` | **Default** |
| `Large` | `space-2 space-4` | `body-md` | `radius-md` | Prominent labels |

#### Blazor Implementation
```razor
<!-- Badge.razor -->
<span class="@ClassNames">@ChildContent</span>

@code {
    [Parameter] public BadgeVariant Variant { get; set; } = BadgeVariant.Default;
    [Parameter] public BadgeSize Size { get; set; } = BadgeSize.Medium;
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public string Class { get; set; }
    
    private string ClassNames => new ClassBuilder()
        .Add("badge")
        .Add($"badge--{Variant.ToString().ToLower()}")
        .Add($"badge--{Size.ToString().ToLower()}")
        .Add(Class)
        .Build();
}
```

#### CSS
```css
.badge {
  display: inline-flex;
  align-items: center;
  font-family: var(--font-family-base);
  font-weight: var(--weight-medium);
  border-radius: var(--radius-md);
  white-space: nowrap;
}

.badge--small {
  padding: var(--space-1) var(--space-2);
  font-size: var(--text-caption);
  border-radius: var(--radius-sm);
}
.badge--medium {
  padding: var(--space-1) var(--space-3);
  font-size: var(--text-body-sm);
}
.badge--large {
  padding: var(--space-2) var(--space-4);
  font-size: var(--text-body-md);
}

/* Rarity variants */
.badge--common { background: var(--color-rarity-common); color: var(--color-on-surface); }
.badge--uncommon { background: var(--color-rarity-uncommon); color: var(--color-on-tertiary); }
.badge--rare { background: var(--color-rarity-rare); color: var(--color-on-info); }
.badge--epic { background: var(--color-rarity-epic); color: #FFFFFF; }
.badge--legendary { background: var(--color-rarity-legendary); color: #FFFFFF; }
.badge--mythical { background: var(--color-rarity-mythical); color: var(--color-on-primary); }
.badge--shiny { 
  background: var(--color-rarity-shiny); 
  color: #FFFFFF;
  animation: shine 2s ease-in-out infinite;
}

/* Type variants - dynamic via style attribute */
.badge--type { color: var(--type-on-color); }

/* Status */
.badge--online { background: var(--color-tertiary); color: var(--color-on-tertiary); }
.badge--pending { background: var(--color-warning); color: #FFFFFF; }
.badge--verified { background: var(--color-tertiary); color: var(--color-on-tertiary); }
.badge--offline { background: var(--color-on-surface-variant); color: var(--color-surface); }

/* Platform */
.badge--twitch { background: var(--color-platform-twitch); color: #FFFFFF; }
.badge--youtube { background: var(--color-platform-youtube); color: #FFFFFF; }
.badge--kick { background: var(--color-platform-kick); color: #000000; }
.badge--discord { background: var(--color-platform-discord); color: #FFFFFF; }

@keyframes shine {
  0%, 100% { opacity: 1; }
  50% { opacity: 0.7; }
}
```

---

### Card

#### Variants
| Variant | Description | Elevation |
|---------|-------------|-----------|
| `Default` | Standard content card | `shadow-sm` |
| `Elevated` | Hoverable, interactive | `shadow-md` → `shadow-lg` on hover |
| `Outlined` | Border only, flat | `border: outline` |
| `Pokemon` | **Special** - Pokémon display | `shadow-sm` + type accent border |
| `Collection` | Grid item, compact | `shadow-sm` |

#### Structure
```razor
<!-- Card.razor -->
<div class="@ClassNames" @onclick="OnClick">
    <div class="card__media">
        @MediaContent
    </div>
    <div class="card__content">
        @if (Header != null) {
            <div class="card__header">@Header</div>
        }
        <div class="card__body">@ChildContent</div>
        @if (Actions != null) {
            <div class="card__actions">@Actions</div>
        }
    </div>
</div>
```

#### CSS
```css
.card {
  display: flex;
  flex-direction: column;
  background: var(--color-surface);
  border-radius: var(--radius-lg);
  overflow: hidden;
  transition: all var(--duration-normal) var(--ease-standard);
}

.card--default { box-shadow: var(--shadow-sm); }
.card--outlined { 
  box-shadow: none; 
  border: 1px solid var(--color-outline); 
}
.card--elevated { box-shadow: var(--shadow-sm); }
.card--elevated:hover { 
  box-shadow: var(--shadow-lg); 
  transform: translateY(-2px); 
}

/* Pokémon Card Specific */
.card--pokemon {
  position: relative;
  box-shadow: var(--shadow-sm);
}
.card--pokemon::before {
  content: '';
  position: absolute;
  top: 0; left: 0; right: 0;
  height: 4px;
  background: var(--type-color);
  opacity: 0.8;
}
.card--pokemon:hover {
  box-shadow: var(--shadow-md);
}

.card__media {
  aspect-ratio: 4/3;
  object-fit: cover;
  background: var(--color-surface-variant);
}
.card__content {
  display: flex;
  flex-direction: column;
  flex: 1;
  padding: var(--space-4);
  gap: var(--space-3);
}
.card__header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: var(--space-2);
}
.card__body { flex: 1; }
.card__actions {
  display: flex;
  gap: var(--space-2);
  padding-top: var(--space-2);
  border-top: 1px solid var(--color-outline-variant);
}
```

---

### PokemonCard (Organism)

#### Props
```csharp
public class PokemonCardParams {
    public PokemonDto Pokemon { get; set; }           // Required
    public CardVariant Variant { get; set; } = CardVariant.Pokemon;
    public bool ShowStats { get; set; } = true;
    public bool ShowActions { get; set; } = true;
    public bool IsInCollection { get; set; } = false;
    public bool IsShiny { get; set; } = false;
    public EventCallback<PokemonDto> OnCapture { get; set; }
    public EventCallback<PokemonDto> OnView { get; set; }
    public EventCallback<PokemonDto> OnTrade { get; set; }
}
```

#### Layout
```
┌─────────────────────────────────────┐
│  Type Badge(s)          Rarity Badge │  ← Header
├─────────────────────────────────────┤
│                                     │
│        Pokémon Sprite/Art           │  ← Media (aspect-ratio: 1/1)
│                                     │
├─────────────────────────────────────┤
│  #025  Pikachu            ⚡ Electric│  ← Name + ID
│  35/35 HP    55 ATK    40 DEF       │  ← StatBar (if ShowStats)
├─────────────────────────────────────┤
│  [Capture]  [View]  [Trade]         │  ← Actions (if ShowActions)
└─────────────────────────────────────┘
```

#### CSS
```css
.pokemon-card {
  /* Extends .card--pokemon */
}
.pokemon-card__header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
}
.pokemon-card__name-row {
  display: flex;
  align-items: baseline;
  gap: var(--space-2);
  flex-wrap: wrap;
}
.pokemon-card__id {
  font-family: var(--font-family-mono);
  font-size: var(--text-body-sm);
  color: var(--color-on-surface-variant);
}
.pokemon-card__name {
  font-size: var(--text-heading-md);
  font-weight: var(--weight-semibold);
  color: var(--color-on-surface);
}
.pokemon-card__type-badge { /* Badge component */ }
.pokemon-card__stats {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: var(--space-2);
}
.pokemon-card__stat { /* StatBar component */ }
.pokemon-card__actions {
  display: flex;
  gap: var(--space-2);
}
.pokemon-card__actions .btn { flex: 1; }
```

---

### CaptureButton (Special Molecule)

#### Behavior
1. **Idle**: Pulsing glow animation
2. **Hover**: Scale up, intensify glow
3. **Click**: "Throw" animation → shake → result
4. **Cooldown**: Disabled, circular progress ring
5. **Success**: Celebration animation (confetti, sparkle)
6. **Failed**: Shake + error color → back to idle

#### States & Timing
| State | Duration | Animation |
|-------|----------|-----------|
| Idle pulse | 2s infinite | `box-shadow` pulse |
| Throw | 600ms | `transform: translateY(-20px) rotate(-15deg)` |
| Shake | 300ms | `transform: translateX(±8px)` |
| Celebration | 1500ms | Confetti + scale pulse |
| Cooldown | Configurable (5-30s) | Circular progress ring |

#### Blazor Implementation
```razor
<!-- CaptureButton.razor -->
<button class="@ClassNames" 
        disabled="@(State != CaptureState.Idle)" 
        @onclick="HandleClick">
    <div class="capture-btn__inner">
        @if (State == CaptureState.Cooldown) {
            <ProgressRing Value="@CooldownProgress" Size="ProgressRingSize.Large" />
            <span class="capture-btn__cooldown-text">@CooldownText</span>
        } else {
            <Icon Name="pokeball" Class="capture-btn__icon" />
            <span class="capture-btn__text">@ButtonText</span>
        }
    </div>
</button>

@code {
    public enum CaptureState { Idle, Throwing, Celebrating, Cooldown, Failed }
    
    [Parameter] public CaptureState State { get; set; } = CaptureState.Idle;
    [Parameter] public int CooldownSeconds { get; set; } = 10;
    [Parameter] public EventCallback OnCapture { get; set; }
    
    private CaptureState _internalState;
    private double _cooldownProgress = 1;
    private string _cooldownText = "";
    
    protected override void OnParametersSet() {
        _internalState = State;
        if (State == CaptureState.Cooldown) {
            StartCooldown();
        }
    }
    
    private async Task HandleClick() {
        if (_internalState != CaptureState.Idle) return;
        
        _internalState = CaptureState.Throwing;
        StateHasChanged();
        await Task.Delay(600); // Throw animation
        
        // Trigger capture logic
        await OnCapture.InvokeAsync();
        
        // Result handled by parent via State parameter
    }
    
    private async void StartCooldown() {
        var start = DateTime.Now;
        var end = start.AddSeconds(CooldownSeconds);
        
        while (DateTime.Now < end && _internalState == CaptureState.Cooldown) {
            var remaining = (end - DateTime.Now).TotalSeconds;
            _cooldownProgress = remaining / CooldownSeconds;
            _cooldownText = $"{Math.Ceiling(remaining)}s";
            StateHasChanged();
            await Task.Delay(100);
        }
        
        if (_internalState == CaptureState.Cooldown) {
            _internalState = CaptureState.Idle;
            _cooldownProgress = 1;
            _cooldownText = "";
            StateHasChanged();
        }
    }
}
```

#### CSS
```css
.capture-btn {
  position: relative;
  width: 72px;
  height: 72px;
  border-radius: var(--radius-full);
  background: var(--gradient-capture);
  border: none;
  box-shadow: var(--shadow-glow);
  cursor: pointer;
  overflow: hidden;
  animation: pulse-glow 2s ease-in-out infinite;
}

.capture-btn:focus-visible {
  outline: 3px solid var(--color-primary);
  outline-offset: 3px;
}

.capture-btn:disabled {
  opacity: var(--opacity-disabled);
  cursor: wait;
  animation: none;
}

.capture-btn__inner {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
  gap: var(--space-1);
}

.capture-btn__icon {
  width: 28px;
  height: 28px;
  animation: bob 1.5s ease-in-out infinite;
}

.capture-btn__text {
  font-size: var(--text-caption);
  font-weight: var(--weight-medium);
  color: var(--color-on-secondary);
  text-transform: uppercase;
  letter-spacing: var(--tracking-widest);
}

.capture-btn__cooldown-text {
  font-family: var(--font-family-mono);
  font-size: var(--text-caption);
  color: var(--color-on-secondary);
}

/* Throw Animation */
.capture-btn--throwing {
  animation: throw-pokeball 600ms var(--ease-spring) forwards;
}

@keyframes throw-pokeball {
  0% { transform: translateY(0) rotate(0deg) scale(1); }
  50% { transform: translateY(-30px) rotate(-20deg) scale(1.1); }
  100% { transform: translateY(-40px) rotate(-360deg) scale(0.8); opacity: 0; }
}

/* Celebration */
.capture-btn--celebrating {
  animation: celebrate 1500ms var(--ease-spring) forwards;
}

@keyframes celebrate {
  0% { transform: scale(1); box-shadow: var(--shadow-glow); }
  25% { transform: scale(1.2); box-shadow: var(--shadow-glow-strong); }
  50% { transform: scale(0.95); }
  75% { transform: scale(1.1); }
  100% { transform: scale(1); box-shadow: var(--shadow-glow); }
}

/* Cooldown Progress Ring */
.capture-btn__progress-ring {
  position: absolute;
  inset: 0;
  border-radius: var(--radius-full);
}

.capture-btn__progress-ring circle {
  transform: rotate(-90deg);
  transform-origin: center;
}

/* Pulse Glow */
@keyframes pulse-glow {
  0%, 100% { box-shadow: var(--shadow-glow); }
  50% { box-shadow: var(--shadow-glow-strong); }
}

/* Bob Animation */
@keyframes bob {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-3px); }
}
```

---

### SpawnNotification (Organism)

#### Behavior
- **Rare+**: Full-screen overlay with animation
- **Uncommon**: Toast-style slide-in
- **Common**: Subtle indicator in collection header

#### Layout (Rare)
```
┌─────────────────────────────────────┐
│  ═════════════════════════════════  │  ← Top glow bar
│                                     │
│        ✨  RARE SPAWN!  ✨          │  ← Animated headline
│                                     │
│        [Large Pokémon Art]          │
│                                     │
│        Pikachu  ⚡ Electric         │  ← Name + Type
│        #025                         │
│                                     │
│    [Capture Now]   [Dismiss]        │  ← Actions
│                                     │
│  ═════════════════════════════════  │  ← Bottom glow bar
└─────────────────────────────────────┘
```

#### CSS
```css
.spawn-notification {
  position: fixed;
  inset: 0;
  z-index: var(--z-toast);
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(13, 17, 23, 0.95);
  backdrop-filter: blur(8px);
  animation: fade-in var(--duration-normal) var(--ease-standard);
}

.spawn-notification__content {
  position: relative;
  max-width: 400px;
  width: 90%;
  padding: var(--space-8);
  background: var(--color-surface);
  border-radius: var(--radius-xl);
  box-shadow: var(--shadow-xl), var(--shadow-glow-strong);
  border: 2px solid var(--color-primary);
  text-align: center;
  animation: slide-up var(--duration-slow) var(--ease-spring);
}

.spawn-notification--rare .spawn-notification__content {
  border-color: var(--color-rarity-rare);
}
.spawn-notification--epic .spawn-notification__content {
  border-color: var(--color-rarity-epic);
}
.spawn-notification--legendary .spawn-notification__content {
  border-color: var(--color-rarity-legendary);
}
.spawn-notification--mythical .spawn-notification__content {
  border-color: var(--color-rarity-mythical);
  animation: mythical-border 3s linear infinite;
}

.spawn-notification__headline {
  font-family: var(--font-family-display);
  font-size: var(--text-heading-lg);
  color: var(--color-primary);
  margin-bottom: var(--space-4);
  animation: glow-text 1.5s ease-in-out infinite;
}

.spawn-notification__pokemon-art {
  width: 200px;
  height: 200px;
  margin: 0 auto var(--space-6);
  animation: float 3s ease-in-out infinite;
}

.spawn-notification__name {
  font-size: var(--text-display-md);
  font-weight: var(--weight-bold);
  color: var(--color-on-surface);
  margin-bottom: var(--space-1);
}

.spawn-notification__type {
  display: inline-flex;
  gap: var(--space-2);
  margin-bottom: var(--space-6);
}

.spawn-notification__actions {
  display: flex;
  gap: var(--space-3);
  justify-content: center;
}

@keyframes fade-in {
  from { opacity: 0; }
  to { opacity: 1; }
}

@keyframes slide-up {
  from { opacity: 0; transform: translateY(30px) scale(0.95); }
  to { opacity: 1; transform: translateY(0) scale(1); }
}

@keyframes glow-text {
  0%, 100% { text-shadow: 0 0 8px var(--color-primary); }
  50% { text-shadow: 0 0 20px var(--color-primary), 0 0 30px var(--color-primary); }
}

@keyframes float {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-8px); }
}

@keyframes mythical-border {
  0% { border-color: var(--color-rarity-mythical); }
  25% { border-color: var(--color-rarity-shiny); }
  50% { border-color: var(--color-rarity-legendary); }
  75% { border-color: var(--color-rarity-epic); }
  100% { border-color: var(--color-rarity-mythical); }
}
```

---

## Platform-Specific Adaptations

### MAUI Overlay Adjustments

| Component | Web | MAUI Overlay |
|-----------|-----|--------------|
| `Button` | Full variants | Primary, Capture, Ghost only |
| `Card` | Hover states | Tap states, no hover |
| `SpawnNotification` | Modal overlay | Native toast / overlay window |
| `Tooltip` | Hover | Long-press (500ms) |
| `Dropdown` | Click | Native picker / action sheet |
| `Tabs` | Horizontal | Segmented control |
| `ProgressRing` | SVG | Native `ProgressBar` |
| `Modal` | Centered | Bottom sheet (iOS) / Dialog (Android) |

### MAUI Style Overrides
```xml
<!-- OverrideStyles.xaml -->
<ResourceDictionary>
  <!-- Buttons: larger touch targets -->
  <Style TargetType="Button" Class="btn--medium">
    <Setter Property="HeightRequest" Value="48" />
    <Setter Property="Padding" Value="24,12" />
    <Setter Property="FontSize" Value="18" />
  </Style>
  
  <!-- Capture button: even larger -->
  <Style TargetType="Button" Class="btn--capture">
    <Setter Property="WidthRequest" Value="88" />
    <Setter Property="HeightRequest" Value="88" />
    <Setter Property="CornerRadius" Value="44" />
  </Style>
  
  <!-- Cards: no hover, tap feedback -->
  <Style TargetType="Frame" Class="card--elevated">
    <Setter Property="HasShadow" Value="True" />
    <Setter Property="Padding" Value="16" />
  </Style>
  
  <!-- Typography: system font scaling -->
  <Style TargetType="Label" Class="text-body-md">
    <Setter Property="FontSize" Value="16" />
    <Setter Property="LineHeight" Value="1.6" />
  </Style>
</ResourceDictionary>
```

---

## Accessibility Checklist (Per Component)

### Button
- [ ] `role="button"` (native `<button>`)
- [ ] `aria-disabled` when disabled
- [ ] `aria-pressed` for toggle buttons
- [ ] Focus visible: 2px outline + 2px offset
- [ ] Keyboard: Enter/Space activates
- [ ] Loading: `aria-busy="true"`, `aria-live="polite"`

### Badge
- [ ] `role="status"` for dynamic badges
- [ ] Color not sole indicator (icon + text)
- [ ] Sufficient contrast (verified in COLORS.md)

### Card
- [ ] Interactive cards: `tabindex="0"`, `role="button"`
- [ ] Focus outline on entire card
- [ ] `aria-label` for image-only cards
- [ ] Heading structure inside card

### PokemonCard
- [ ] Pokémon name as heading (`h3`)
- [ ] Sprite: `alt="Pikachu, Electric type"`
- [ ] Stats: `role="list"`, each stat `role="listitem"`
- [ ] Actions: grouped in `role="group"`, `aria-label="Pokemon actions"`

### CaptureButton
- [ ] `aria-label="Capture Pikachu, 10 second cooldown"`
- [ ] `aria-busy` during throw animation
- [ ] `aria-live="polite"` for result announcement
- [ ] Cooldown: `aria-valuemin="0"`, `aria-valuemax="10"`, `aria-valuenow="7"`

### SpawnNotification
- [ ] `role="alertdialog"`, `aria-modal="true"`
- [ ] `aria-labelledby="spawn-title"`, `aria-describedby="spawn-desc"`
- [ ] Focus trap while open
- [ ] Escape key dismisses
- [ ] `aria-live="assertive"` for rare+ spawns

---

## Testing Guidelines

### Visual Regression
```bash
# Storybook + Chromatic
npm run storybook:build
npx chromatic --project-token=<token>
```

### Unit Tests (Blazor)
```csharp
// ButtonTests.razor
[Fact]
public void Button_RendersPrimaryVariant() {
    var cut = RenderComponent<Button>(p => p
        .Add(p => p.Variant, ButtonVariant.Primary)
        .Add(p => p.ChildContent, "Click me"));
    
    cut.Find("button").ClassList.Should().Contain("btn--primary");
}

[Fact]
public async Task Button_OnClick_FiresCallback() {
    var clicked = false;
    var cut = RenderComponent<Button>(p => p
        .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true)));
    
    await cut.Find("button").ClickAsync(new MouseEventArgs());
    clicked.Should().BeTrue();
}
```

### Accessibility Tests
```javascript
// a11y.test.js
import { render, screen } from '@testing-library/blazor';
import { axe } from 'jest-axe';

test('Button has no a11y violations', async () => {
    const { container } = render(<Button>Click me</Button>);
    const results = await axe(container);
    expect(results).toHaveNoViolations();
});
```

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | 2026-11-17 | Initial component specifications |

---

## Related Documents
- [BRAND.md](BRAND.md) — Brand identity
- [COLORS.md](COLORS.md) — Color tokens & contrast
- [TYPOGRAPHY.md](TYPOGRAPHY.md) — Typography tokens
- [DESIGN-TOKENS.md](DESIGN-TOKENS.md) — Complete token reference
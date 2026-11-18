using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PokeStream.UI.Web.Theme;

/// <summary>
/// Theme state provider for UI components (e.g., ThemeToggle).
/// Tracks current theme (dark/light) via JavaScript interop.
/// MudBlazor handles actual palette switching via data-mud-theme attribute.
/// </summary>
public class PokestreamThemeProvider : ComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JS { get; set; }

    private bool _isDark = true;
    private DotNetObjectReference<PokestreamThemeProvider>? _dotNetRef;
    private bool _initialized = false;

    public bool IsDark => _isDark;

    public event Action? OnThemeChanged;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !_initialized && JS != null)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("pokestreamTheme.init", _dotNetRef);
            _isDark = await JS.InvokeAsync<bool>("pokestreamTheme.getIsDark");
            _initialized = true;
            OnThemeChanged?.Invoke();
            StateHasChanged();
        }
    }

    [JSInvokable]
    public void OnThemeChangedFromJS(bool isDark)
    {
        _isDark = isDark;
        OnThemeChanged?.Invoke();
        StateHasChanged();
    }

    public async Task ToggleThemeAsync()
    {
        if (JS == null) return;

        var newIsDark = !_isDark;
        await JS.InvokeVoidAsync("pokestreamTheme.setIsDark", newIsDark);
    }

    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}
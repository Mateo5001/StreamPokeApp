/**
 * Theme Toggle Module
 * Handles theme persistence and synchronization across tabs
 */
window.themeToggle = {
    _dotNetRef: null,
    _mediaQuery: null,

    init(dotNetRef) {
        this._dotNetRef = dotNetRef;

        // Check for saved theme preference or system preference
        const savedTheme = localStorage.getItem('pokestream-theme');
        const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        const isDark = savedTheme ? savedTheme === 'dark' : prefersDark;

        this.applyTheme(isDark);

        // Listen for system theme changes
        this._mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');
        this._mediaQuery.addEventListener('change', this.onSystemThemeChange.bind(this));

        // Listen for storage changes (sync across tabs)
        window.addEventListener('storage', this.onStorageChange.bind(this));

        return isDark;
    },

    getTheme() {
        const savedTheme = localStorage.getItem('pokestream-theme');
        const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        return savedTheme ? savedTheme === 'dark' : prefersDark;
    },

    setTheme(isDark) {
        this.applyTheme(isDark);
        localStorage.setItem('pokestream-theme', isDark ? 'dark' : 'light');
    },

    applyTheme(isDark) {
        document.documentElement.setAttribute('data-theme', isDark ? 'dark' : 'light');

        // Update meta theme-color for mobile browsers
        const metaThemeColor = document.querySelector('meta[name="theme-color"]');
        if (metaThemeColor) {
            metaThemeColor.setAttribute('content', isDark ? '#0A0E17' : '#F8FAFC');
        }
    },

    onSystemThemeChange(e) {
        // Only auto-switch if user hasn't explicitly set a preference
        if (!localStorage.getItem('pokestream-theme')) {
            const isDark = e.matches;
            this.applyTheme(isDark);
            this._dotNetRef?.invokeMethodAsync('OnThemeChanged', isDark);
        }
    },

    onStorageChange(e) {
        if (e.key === 'pokestream-theme' && e.newValue !== e.oldValue) {
            const isDark = e.newValue === 'dark';
            this.applyTheme(isDark);
            this._dotNetRef?.invokeMethodAsync('OnThemeChanged', isDark);
        }
    },

    dispose() {
        if (this._mediaQuery) {
            this._mediaQuery.removeEventListener('change', this.onSystemThemeChange.bind(this));
        }
        window.removeEventListener('storage', this.onStorageChange.bind(this));
        this._dotNetRef = null;
    }
};
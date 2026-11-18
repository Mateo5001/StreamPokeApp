// PokéStream Theme Persistence
// Handles system preference detection, localStorage persistence, and theme switching
// Also updates MudBlazor CSS variables directly for reliable theme switching

(function () {
    'use strict';

    const THEME_KEY = 'pokestream-theme';
    const DARK_CLASS = 'dark';
    const LIGHT_CLASS = 'light';
    const DATA_THEME_ATTR = 'data-theme';
    const DATA_MUD_THEME_ATTR = 'data-mud-theme';

    let dotNetRef = null;

    // MudBlazor CSS variable overrides for dark/light themes
    // These match the PaletteDark and PaletteLight from PokestreamTheme.cs
    const MUD_VARIABLES = {
        dark: {
            '--mud-palette-primary': 'rgba(255,140,0,1)',
            '--mud-palette-primary-rgb': '255,140,0',
            '--mud-palette-primary-text': 'rgba(0,0,0,1)',
            '--mud-palette-primary-darken': 'rgb(204,112,0)',
            '--mud-palette-primary-lighten': 'rgb(255,156,26)',
            '--mud-palette-primary-hover': 'rgba(255,140,0,0.058823529411764705)',
            '--mud-palette-secondary': 'rgba(13,27,42,1)',
            '--mud-palette-secondary-rgb': '13,27,42',
            '--mud-palette-secondary-text': 'rgba(255,255,255,1)',
            '--mud-palette-secondary-darken': 'rgb(5,10,16)',
            '--mud-palette-secondary-lighten': 'rgb(22,45,70)',
            '--mud-palette-secondary-hover': 'rgba(13,27,42,0.058823529411764705)',
            '--mud-palette-tertiary': 'rgba(63,185,80,1)',
            '--mud-palette-tertiary-rgb': '63,185,80',
            '--mud-palette-tertiary-text': 'rgba(255,255,255,1)',
            '--mud-palette-tertiary-darken': 'rgb(53,155,67)',
            '--mud-palette-tertiary-lighten': 'rgb(86,211,100)',
            '--mud-palette-tertiary-hover': 'rgba(63,185,80,0.058823529411764705)',
            '--mud-palette-info': 'rgba(88,166,255,1)',
            '--mud-palette-info-rgb': '88,166,255',
            '--mud-palette-info-text': 'rgba(255,255,255,1)',
            '--mud-palette-info-darken': 'rgb(74,140,216)',
            '--mud-palette-info-lighten': 'rgb(106,183,255)',
            '--mud-palette-info-hover': 'rgba(88,166,255,0.058823529411764705)',
            '--mud-palette-success': 'rgba(63,185,80,1)',
            '--mud-palette-success-rgb': '63,185,80',
            '--mud-palette-success-text': 'rgba(255,255,255,1)',
            '--mud-palette-success-darken': 'rgb(53,155,67)',
            '--mud-palette-success-lighten': 'rgb(86,211,100)',
            '--mud-palette-success-hover': 'rgba(63,185,80,0.058823529411764705)',
            '--mud-palette-warning': 'rgba(247,129,102,1)',
            '--mud-palette-warning-rgb': '247,129,102',
            '--mud-palette-warning-text': 'rgba(255,255,255,1)',
            '--mud-palette-warning-darken': 'rgb(209,109,86)',
            '--mud-palette-warning-lighten': 'rgb(248,150,125)',
            '--mud-palette-warning-hover': 'rgba(247,129,102,0.058823529411764705)',
            '--mud-palette-error': 'rgba(248,81,73,1)',
            '--mud-palette-error-rgb': '248,81,73',
            '--mud-palette-error-text': 'rgba(255,255,255,1)',
            '--mud-palette-error-darken': 'rgb(210,68,61)',
            '--mud-palette-error-lighten': 'rgb(249,107,99)',
            '--mud-palette-error-hover': 'rgba(248,81,73,0.058823529411764705)',
            '--mud-palette-dark': 'rgba(66,66,66,1)',
            '--mud-palette-dark-rgb': '66,66,66',
            '--mud-palette-dark-text': 'rgba(255,255,255,1)',
            '--mud-palette-dark-darken': 'rgb(46,46,46)',
            '--mud-palette-dark-lighten': 'rgb(87,87,87)',
            '--mud-palette-dark-hover': 'rgba(66,66,66,0.058823529411764705)',
            '--mud-palette-text-primary': 'rgba(230,237,243,1)',
            '--mud-palette-text-primary-rgb': '230,237,243',
            '--mud-palette-text-secondary': 'rgba(148,163,184,1)',
            '--mud-palette-text-secondary-rgb': '148,163,184',
            '--mud-palette-text-disabled': 'rgba(255,255,255,0.38)',
            '--mud-palette-text-disabled-rgb': '255,255,255',
            '--mud-palette-action-default': 'rgba(148,163,184,1)',
            '--mud-palette-action-default-hover': 'rgba(148,163,184,0.058823529411764705)',
            '--mud-palette-action-disabled': 'rgba(148,163,184,0.3764705882352941)',
            '--mud-palette-action-disabled-background': 'rgba(148,163,184,0.11764705882352941)',
            '--mud-palette-surface': 'rgba(17,24,39,1)',
            '--mud-palette-surface-rgb': '17,24,39',
            '--mud-palette-background': 'rgba(10,14,23,1)',
            '--mud-palette-background-gray': 'rgba(17,24,39,1)',
            '--mud-palette-drawer-background': 'rgba(17,24,39,1)',
            '--mud-palette-drawer-text': 'rgba(230,237,243,1)',
            '--mud-palette-drawer-icon': 'rgba(148,163,184,1)',
            '--mud-palette-appbar-background': 'rgba(17,24,39,1)',
            '--mud-palette-appbar-text': 'rgba(230,237,243,1)',
            '--mud-palette-lines-default': 'rgba(51,65,85,1)',
            '--mud-palette-lines-inputs': 'rgba(51,65,85,1)',
            '--mud-palette-table-lines': 'rgba(51,65,85,1)',
            '--mud-palette-table-striped': 'rgba(255,255,255,0.0196078431372549)',
            '--mud-palette-table-hover': 'rgba(255,255,255,0.0392156862745098)',
            '--mud-palette-divider': 'rgba(51,65,85,1)',
            '--mud-palette-divider-rgb': '51,65,85',
            '--mud-palette-divider-light': 'rgba(255,255,255,0.058823529411764705)',
            '--mud-palette-skeleton': 'rgba(255,255,255,0.10980392156862745)',
            '--mud-palette-gray-default': '#94A3B8',
            '--mud-palette-gray-light': '#475569',
            '--mud-palette-gray-lighter': '#334155',
            '--mud-palette-gray-dark': '#111827',
            '--mud-palette-gray-darker': '#0A0E17',
            '--mud-palette-overlay-dark': 'rgba(0,0,0,0.4980392156862745)',
            '--mud-palette-overlay-light': 'rgba(255,255,255,0.4980392156862745)',
            '--mud-palette-border-opacity': '1',
        },
        light: {
            '--mud-palette-primary': 'rgba(232,108,0,1)',
            '--mud-palette-primary-rgb': '232,108,0',
            '--mud-palette-primary-text': 'rgba(255,255,255,1)',
            '--mud-palette-primary-darken': 'rgb(194,90,0)',
            '--mud-palette-primary-lighten': 'rgb(255,124,10)',
            '--mud-palette-primary-hover': 'rgba(232,108,0,0.058823529411764705)',
            '--mud-palette-secondary': 'rgba(13,27,42,1)',
            '--mud-palette-secondary-rgb': '13,27,42',
            '--mud-palette-secondary-text': 'rgba(255,255,255,1)',
            '--mud-palette-secondary-darken': 'rgb(5,10,16)',
            '--mud-palette-secondary-lighten': 'rgb(22,45,70)',
            '--mud-palette-secondary-hover': 'rgba(13,27,42,0.058823529411764705)',
            '--mud-palette-tertiary': 'rgba(40,167,69,1)',
            '--mud-palette-tertiary-rgb': '40,167,69',
            '--mud-palette-tertiary-text': 'rgba(255,255,255,1)',
            '--mud-palette-tertiary-darken': 'rgb(34,140,58)',
            '--mud-palette-tertiary-lighten': 'rgb(48,197,83)',
            '--mud-palette-tertiary-hover': 'rgba(40,167,69,0.058823529411764705)',
            '--mud-palette-info': 'rgba(3,102,214,1)',
            '--mud-palette-info-rgb': '3,102,214',
            '--mud-palette-info-text': 'rgba(255,255,255,1)',
            '--mud-palette-info-darken': 'rgb(3,86,181)',
            '--mud-palette-info-lighten': 'rgb(4,119,251)',
            '--mud-palette-info-hover': 'rgba(3,102,214,0.058823529411764705)',
            '--mud-palette-success': 'rgba(0,200,83,1)',
            '--mud-palette-success-rgb': '0,200,83',
            '--mud-palette-success-text': 'rgba(255,255,255,1)',
            '--mud-palette-success-darken': 'rgb(0,163,68)',
            '--mud-palette-success-lighten': 'rgb(0,235,98)',
            '--mud-palette-success-hover': 'rgba(0,200,83,0.058823529411764705)',
            '--mud-palette-warning': 'rgba(230,81,0,1)',
            '--mud-palette-warning-rgb': '230,81,0',
            '--mud-palette-warning-text': 'rgba(255,255,255,1)',
            '--mud-palette-warning-darken': 'rgb(194,68,0)',
            '--mud-palette-warning-lighten': 'rgb(255,96,10)',
            '--mud-palette-warning-hover': 'rgba(230,81,0,0.058823529411764705)',
            '--mud-palette-error': 'rgba(198,40,40,1)',
            '--mud-palette-error-rgb': '198,40,40',
            '--mud-palette-error-text': 'rgba(255,255,255,1)',
            '--mud-palette-error-darken': 'rgb(165,34,34)',
            '--mud-palette-error-lighten': 'rgb(215,60,60)',
            '--mud-palette-error-hover': 'rgba(198,40,40,0.058823529411764705)',
            '--mud-palette-dark': 'rgba(66,66,66,1)',
            '--mud-palette-dark-rgb': '66,66,66',
            '--mud-palette-dark-text': 'rgba(255,255,255,1)',
            '--mud-palette-dark-darken': 'rgb(46,46,46)',
            '--mud-palette-dark-lighten': 'rgb(87,87,87)',
            '--mud-palette-dark-hover': 'rgba(66,66,66,0.058823529411764705)',
            '--mud-palette-text-primary': 'rgba(15,23,42,1)',
            '--mud-palette-text-primary-rgb': '15,23,42',
            '--mud-palette-text-secondary': 'rgba(71,85,105,1)',
            '--mud-palette-text-secondary-rgb': '71,85,105',
            '--mud-palette-text-disabled': 'rgba(0,0,0,0.3764705882352941)',
            '--mud-palette-text-disabled-rgb': '0,0,0',
            '--mud-palette-action-default': 'rgba(100,116,139,1)',
            '--mud-palette-action-default-hover': 'rgba(100,116,139,0.058823529411764705)',
            '--mud-palette-action-disabled': 'rgba(100,116,139,0.3764705882352941)',
            '--mud-palette-action-disabled-background': 'rgba(100,116,139,0.11764705882352941)',
            '--mud-palette-surface': 'rgba(255,255,255,1)',
            '--mud-palette-surface-rgb': '255,255,255',
            '--mud-palette-background': 'rgba(255,253,248,1)',
            '--mud-palette-background-gray': 'rgba(248,245,238,1)',
            '--mud-palette-drawer-background': 'rgba(255,255,255,1)',
            '--mud-palette-drawer-text': 'rgba(15,23,42,1)',
            '--mud-palette-drawer-icon': 'rgba(97,97,97,1)',
            '--mud-palette-appbar-background': 'rgba(255,255,255,1)',
            '--mud-palette-appbar-text': 'rgba(15,23,42,1)',
            '--mud-palette-lines-default': 'rgba(226,232,240,1)',
            '--mud-palette-lines-inputs': 'rgba(203,213,225,1)',
            '--mud-palette-table-lines': 'rgba(224,224,224,1)',
            '--mud-palette-table-striped': 'rgba(0,0,0,0.0196078431372549)',
            '--mud-palette-table-hover': 'rgba(0,0,0,0.0392156862745098)',
            '--mud-palette-divider': 'rgba(226,232,240,1)',
            '--mud-palette-divider-rgb': '226,232,240',
            '--mud-palette-divider-light': 'rgba(0,0,0,0.058823529411764705)',
            '--mud-palette-skeleton': 'rgba(0,0,0,0.10980392156862745)',
            '--mud-palette-gray-default': '#64748B',
            '--mud-palette-gray-light': '#CBD5E1',
            '--mud-palette-gray-lighter': '#E2E8F0',
            '--mud-palette-gray-dark': '#FFFDF8',
            '--mud-palette-gray-darker': '#F8F5EE',
            '--mud-palette-overlay-dark': 'rgba(33,33,33,0.4980392156862745)',
            '--mud-palette-overlay-light': 'rgba(255,255,255,0.4980392156862745)',
            '--mud-palette-border-opacity': '1',
        }
    };

    // Initialize theme system
    window.themeToggleAttach = function (dotNetRef) {
        if (dotNetRef) {
            window.themeToggleDotNetRef = dotNetRef;
            // Attach click handler directly to the button
            const toggleBtn = document.querySelector('.theme-toggle');
            if (toggleBtn) {
                toggleBtn.addEventListener('click', function () {
                    if (window.themeToggleDotNetRef) {
                        window.themeToggleDotNetRef.invokeMethodAsync('OnJsToggle');
                    }
                });
            }
        }
    };

    window.toggleTheme = function () {
        if (window.themeToggleDotNetRef) {
            window.themeToggleDotNetRef.invokeMethodAsync('OnJsToggle');
        }
    };

    window.toggleThemeKeydown = function (event) {
        if (event.key === 'Enter' || event.key === ' ') {
            event.preventDefault();
            window.toggleTheme();
        }
    };

    window.pokestreamTheme = {
        init: function (dotNetReference) {
            dotNetRef = dotNetReference;
            const theme = getInitialTheme();
            applyTheme(theme);
            applyMudVariables(theme === DARK_CLASS);
            // Attach theme toggle click handler when button is ready
            function attachToggleHandler() {
                const toggleBtn = document.querySelector('.theme-toggle');
                if (toggleBtn) {
                    toggleBtn.addEventListener('click', function () {
                        if (window.themeToggleDotNetRef) {
                            window.themeToggleDotNetRef.invokeMethodAsync('OnJsToggle');
                        }
                    });
                } else {
                    // Retry after a short delay
                    setTimeout(attachToggleHandler, 100);
                }
            }
            attachToggleHandler();
        },

        getIsDark: function () {
            return getInitialTheme() === DARK_CLASS;
        },

        setIsDark: function (isDark) {
            const theme = isDark ? DARK_CLASS : LIGHT_CLASS;
            localStorage.setItem(THEME_KEY, theme);
            applyTheme(theme);
            applyMudVariables(isDark);
            if (dotNetRef) {
                dotNetRef.invokeMethodAsync('OnThemeChangedFromJS', isDark);
            }
        }
    };

    function getInitialTheme() {
        // 1. Check localStorage
        const stored = localStorage.getItem(THEME_KEY);
        if (stored === DARK_CLASS || stored === LIGHT_CLASS) {
            return stored;
        }

        // 2. Check system preference
        if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
            return DARK_CLASS;
        }

        // 3. Default to dark (overlay default)
        return DARK_CLASS;
    }

    function applyTheme(theme) {
        const html = document.documentElement;
        const isDark = theme === DARK_CLASS;

        // Set data attributes for CSS
        html.setAttribute(DATA_THEME_ATTR, theme);
        html.setAttribute(DATA_MUD_THEME_ATTR, theme);

        // Set class for any legacy selectors
        html.classList.remove(DARK_CLASS, LIGHT_CLASS);
        html.classList.add(theme);

        // Update meta theme-color for mobile browsers
        updateMetaThemeColor(isDark);
    }

    function applyMudVariables(isDark) {
        const variables = isDark ? MUD_VARIABLES.dark : MUD_VARIABLES.light;
        const style = document.documentElement.style;
        for (const [key, value] of Object.entries(variables)) {
            style.setProperty(key, value);
        }
    }

    function updateMetaThemeColor(isDark) {
        let metaThemeColor = document.querySelector('meta[name="theme-color"]');
        if (!metaThemeColor) {
            metaThemeColor = document.createElement('meta');
            metaThemeColor.name = 'theme-color';
            document.head.appendChild(metaThemeColor);
        }
        metaThemeColor.content = isDark ? '#0A0E17' : '#FFFDF8';
    }

    // Listen for system theme changes (only if user hasn't set a preference)
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
        const stored = localStorage.getItem(THEME_KEY);
        if (!stored) {
            const isDark = e.matches;
            const newTheme = isDark ? DARK_CLASS : LIGHT_CLASS;
            applyTheme(newTheme);
            applyMudVariables(isDark);
            if (dotNetRef) {
                dotNetRef.invokeMethodAsync('OnThemeChangedFromJS', e.matches);
            }
        }
    });
})();
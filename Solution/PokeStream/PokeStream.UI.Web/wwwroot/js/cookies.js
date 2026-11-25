// Cookie helpers for PokeStream.Core.Services.Cookies.CookieService (loaded as an ES module via IJSRuntime).

export function get(name) {
    const prefix = encodeURIComponent(name) + "=";
    for (const part of document.cookie.split("; ")) {
        if (part.startsWith(prefix)) return decodeURIComponent(part.substring(prefix.length));
    }
    return null;
}

export function set(name, value, days) {
    let cookie = `${encodeURIComponent(name)}=${encodeURIComponent(value)}; path=/; SameSite=Lax`;
    if (days != null) cookie += `; expires=${new Date(Date.now() + days * 864e5).toUTCString()}`;
    if (location.protocol === "https:") cookie += "; Secure";
    document.cookie = cookie;
}

export function remove(name) {
    document.cookie = `${encodeURIComponent(name)}=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT`;
}

// Calls the server endpoint that issues/removes the HttpOnly session cookie.
export async function send(method, url, body) {
    const response = await fetch(url, {
        method,
        credentials: "same-origin",
        headers: body ? { "Content-Type": "application/json" } : undefined,
        body: body ? JSON.stringify(body) : undefined
    });
    if (!response.ok) throw new Error(`Cookie endpoint ${method} ${url} failed: ${response.status}`);
}

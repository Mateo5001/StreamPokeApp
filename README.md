# PokéStream Collector

Overlay interactivo para streamers (Blazor + .NET MAUI) que muestra Pokémon en el stream; los viewers los capturan mediante comandos de chat y gestionan su colección en una web complementaria. Backend compartido sincroniza capturas, verifica automáticamente y persiste la Pokédex de cada usuario entre sesiones y plataformas (Twitch/YouTube/Kick).

---

## 🎮 Características principales

- **Overlay MAUI** — Ventana transparente *always-on-top* que el streamer añade a OBS/Streamlabs como fuente de ventana
- **Captura por chat** — Comandos tipo `!catch`, `!collection`, `!trade` procesados por bot conectado a Twitch/YouTube/Kick
- **Verificación automática** — Sistema de códigos/cooldowns que valida capturas al instante sin intervención manual
- **Web de viewers (Blazor WebAssembly)** — Pokédex personal, historial, intercambios, leaderboards
- **Backend unificado** — ASP.NET Core + SignalR para sincronización en tiempo real entre overlay, bot y web
- **Multi-plataforma** — Una misma colección persiste aunque el viewer cambie de Twitch a YouTube

---

## 🏗 Arquitectura

```
┌─────────────────┐     SignalR / REST      ┌──────────────────┐
│  MAUI Overlay   │ ◄─────────────────────► │  ASP.NET Core    │
│  (Streamer PC)  │                         │  API + Hub       │
└─────────────────┘                         └────────┬─────────┘
                                                      │
                    ┌─────────────────────────────────┼─────────────────────────────────┐
                    ▼                                 ▼                                 ▼
            ┌───────────────┐               ┌─────────────────┐               ┌───────────────┐
            │  Twitch Bot   │               │  YouTube Bot    │               │  Kick Bot     │
            │  (IRC/EventSub)│               │  (LiveChat API) │               │  (WebSocket)  │
            └───────────────┘               └─────────────────┘               └───────────────┘
                    │                                 │                                 │
                    └─────────────────────────────────┼─────────────────────────────────┘
                                                      ▼
                                            ┌─────────────────┐
                                            │  Blazor WASM    │
                                            │  Viewer Web App │
                                            └─────────────────┘
```

---

## 🛠 Tech Stack

| Capa | Tecnología |
|------|------------|
| Overlay Desktop | .NET MAUI (Windows/macOS) |
| Web Viewers | Blazor WebAssembly (PWA) |
| Backend | ASP.NET Core 8, SignalR, EF Core |
| Auth | ASP.NET Core Identity + OAuth (Twitch/Google/Discord) |
| DB | PostgreSQL / SQLite (dev) |
| Bots | TwitchLib, YouTube LiveChat, Kick API |
| Deploy | Docker Compose / Azure Container Apps |

---

## 🚀 Primeros pasos

```bash
# Clonar
git clone https://github.com/tu-usuario/pokestream-collector.git
cd pokestream-collector

# Backend
cd src/PokeStream.Api
dotnet restore
dotnet ef database update
dotnet run

# Overlay MAUI (Windows)
cd ../PokeStream.Overlay
dotnet build -f net8.0-windows10.0.19041

# Web Viewers (Blazor WASM)
cd ../PokeStream.Web
dotnet restore
dotnet run
```

> **Nota:** Requiere .NET 8 SDK y configurar `appsettings.json` con credenciales de Twitch/YouTube/Kick y conexión a BD.

---

## 📄 Licencia

MIT
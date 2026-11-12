# PokéStream Collector - CLAUDE Context

## Project Overview
Interactive overlay for streamers (Blazor + .NET MAUI) showing Pokémon on stream; viewers capture them via chat commands and manage their collection in a companion web app. Shared backend synchronizes captures, auto-verifies, and persists each user's Pokédex across sessions and platforms (Twitch/YouTube/Kick).

## Architecture

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

## Tech Stack

| Layer | Technology |
|-------|------------|
| Overlay Desktop | .NET MAUI (Windows/macOS) |
| Web Viewers | Blazor WebAssembly (PWA) |
| Backend | ASP.NET Core 8, SignalR, EF Core |
| Auth | ASP.NET Core Identity + OAuth (Twitch/Google/Discord) |
| Database | PostgreSQL (prod) / SQLite (dev) |
| Bots | TwitchLib, YouTube LiveChat, Kick API |
| Deploy | Docker Compose / Azure Container Apps |

## Project Structure (src/)
```
src/
├── PokeStream.Api/              # ASP.NET Core API + SignalR Hubs
├── PokeStream.Overlay/          # .NET MAUI Desktop App (Overlay)
├── PokeStream.Web/              # Blazor WASM PWA (Viewer App)
├── PokeStream.Bots/             # Shared library for Twitch/YT/Kick bots
├── PokeStream.Core/             # Domain, DTOs, Interfaces
└── PokeStream.Infrastructure/   # EF Core, Repositories, Services
```

## Development Commands

```bash
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

## Key Requirements
- .NET 8 SDK
- Configure `appsettings.json` with Twitch/YouTube/Kick credentials and database connection
- PostgreSQL for production, SQLite for development

## Coding Guidelines
- Use conventional commits: `feat:`, `fix:`, `chore:`, `docs:`, `refactor:`
- Follow .NET naming conventions (PascalCase for types/methods, camelCase for parameters)
- Use async/await throughout for I/O operations
- SignalR hubs for real-time communication between overlay, bots, and web app
- EF Core migrations for database schema changes
- Shared DTOs in `PokeStream.Core` for API contracts

## Important Notes
- The MAUI overlay runs on the streamer's machine as an always-on-top transparent window
- Bots connect to multiple platforms simultaneously via shared backend
- Viewer collections persist across platforms (same user on Twitch/YouTube/Kick shares one Pokédex)
- Auto-verification system uses codes/cooldowns to validate captures without manual intervention
- Blazor WASM runs as PWA for offline-capable viewer experience
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
| Overlay Desktop | .NET MAUI (Windows/macOS) - *planned* |
| Web Viewers | Blazor WebAssembly (PWA) - *planned* |
| Backend | ASP.NET Core 10, SignalR, EF Core - *API exists* |
| Auth | ASP.NET Core Identity + OAuth (Twitch/Google/Discord) - *planned* |
| Database | PostgreSQL (prod) / SQLite (dev) - *planned* |
| Bots | TwitchLib, YouTube LiveChat, Kick API - *planned* |
| Deploy | Docker Compose / Azure Container Apps - *planned* |
| API Docs | Swagger/OpenAPI (Swashbuckle.AspNetCore 6.6.2) - *configured* |

## Project Structure (Solution/PokeStream/)
```
Solution/PokeStream/
├── PokeStream.slnx              # Solution file
└── PokeStream.API/              # ASP.NET Core API (EXISTS)
    ├── Controllers/
    │   └── WeatherForecastController.cs   # Default controller
    ├── Properties/
    ├── bin/ / obj/
    ├── Program.cs               # Minimal API with OpenAPI
    ├── WeatherForecast.cs       # Default model
    ├── appsettings.json
    ├── appsettings.Development.json
    └── PokeStream.API.csproj    # Target: net10.0, OpenAPI pkg

PLANNED (not created):
├── PokeStream.Overlay/          # .NET MAUI Desktop App (Overlay)
├── PokeStream.Web/              # Blazor WASM PWA (Viewer App)
├── PokeStream.Bots/             # Shared library for Twitch/YT/Kick bots
├── PokeStream.Core/             # Domain, DTOs, Interfaces
└── PokeStream.Infrastructure/   # EF Core, Repositories, Services
```

## Development Commands

```bash
# Backend API (EXISTS - Swagger configured)
cd Solution/PokeStream/PokeStream.API
dotnet restore
dotnet run
# Swagger UI: http://localhost:5000/swagger
# OpenAPI JSON: http://localhost:5000/swagger/v1/swagger.json

# Planned projects - commands TBD after creation
```

## Key Requirements
- .NET 10 SDK (API targets net10.0)
- Configure `appsettings.json` with Twitch/YouTube/Kick credentials and database connection
- PostgreSQL for production, SQLite for development
- Solution root: `Solution/PokeStream/`

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
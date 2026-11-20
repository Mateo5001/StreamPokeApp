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
| Auth | ASP.NET Core Identity + OAuth (Twitch/Google/Discord) - **JWT Auth implemented** |
| Database | Azure SQL Server (prod) - *configured*, SQLite (dev) - *planned* |
| Bots | TwitchLib, YouTube LiveChat, Kick API - *planned* |
| Deploy | Docker Compose / Azure Container Apps - *planned* |
| API Docs | Swagger/OpenAPI (Swashbuckle.AspNetCore 6.6.2) - *configured* |

## Current Auth API Endpoints

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/v1/auth/register` | Register new user | Public |
| POST | `/api/v1/auth/login` | Login (username/email + password) | Public |
| POST | `/api/v1/auth/refresh` | Renew access token using refresh token | Public |
| POST | `/api/v1/auth/logout` | Revoke refresh token | Bearer |
| GET | `/api/v1/auth/me` | Get current user info | Bearer |

## Current Health API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/health/live` | Liveness probe (container orchestration) |
| GET | `/health/ready` | Readiness probe (DB connectivity check) |
| GET | `/health` | Detailed health check with DB info |

## Database Configuration

**Production (Azure SQL Server):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:<YOUR_SERVER>.database.windows.net,1433;Initial Catalog=<YOUR_DB>;Persist Security Info=False;User ID=<YOUR_USER>;Password=<YOUR_PASSWORD>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

**Development (Azure SQL Server - separate DB):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:<YOUR_DEV_SERVER>.database.windows.net,1433;Initial Catalog=<YOUR_DEV_DB>;Persist Security Info=False;User ID=<YOUR_DEV_USER>;Password=<YOUR_DEV_PASSWORD>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

**JWT Settings (Development):**
```json
"Jwt": {
  "Key": "<YOUR_JWT_SECRET_KEY_MIN_32_CHARS>",
  "Issuer": "PokeStreamAPI",
  "Audience": "PokeStreamClient",
  "AccessTokenExpiryMinutes": 15,
  "RefreshTokenExpiryDays": 7
}
```

## Key Requirements
- .NET 10 SDK (API targets net10.0)
- Azure SQL Server configured with SQL Authentication
- `appsettings.json` / `appsettings.Development.json` with connection strings and JWT config
- EF Core Tools for migrations: `dotnet tool install --global dotnet-ef`
- Solution root: `Solution/PokeStream/`

## Coding Guidelines
- Use conventional commits: `feat:`, `fix:`, `chore:`, `docs:`, `refactor:`
- Follow .NET naming conventions (PascalCase for types/methods, camelCase for parameters)
- Use async/await throughout for I/O operations
- SignalR hubs for real-time communication between overlay, bots, and web app
- EF Core migrations for database schema changes
- Shared DTOs in `PokeStream.Core` for API contracts
- Clean Architecture: Core (domain) → EntityData (infrastructure) → API (presentation)

## Icon Guidelines

- **DO NOT** use MudBlazor Material Icons directly in new components 

## Important Notes
- The MAUI overlay runs on the streamer's machine as an always-on-top transparent window
- Bots connect to multiple platforms simultaneously via shared backend
- Viewer collections persist across platforms (same user on Twitch/YouTube/Kick shares one Pokédex)
- Auto-verification system uses codes/cooldowns to validate captures without manual intervention
- Blazor WASM runs as PWA for offline-capable viewer experience
- **Azure SQL**: Configure your server/database in `appsettings.json` (see Database Configuration above)
- **Retry logic enabled** in EF Core for Azure SQL transient failures (EnableRetryOnFailure)
- **Password hashing**: BCrypt via ASP.NET Core Identity PasswordHasher
- **Refresh token rotation**: Old token revoked on each refresh for security
- **Health checks**: Liveness/Readiness probes implemented for container orchestration
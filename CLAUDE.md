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

## Project Structure (Solution/PokeStream/)

```
Solution/PokeStream/
├── PokeStream.sln               # Solution file (classic .sln format)
├── PokeStream.API/              # ASP.NET Core API (EXISTS)
│   ├── Controllers/
│   │   ├── AuthController.cs    # Auth endpoints (register, login, refresh, logout, me)
│   │   └── WeatherForecastController.cs   # Default controller (to remove)
│   ├── Properties/
│   ├── bin/ / obj/
│   ├── Program.cs               # Minimal API with JWT Auth + Swagger
│   ├── WeatherForecast.cs       # Default model (to remove)
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── PokeStream.API.csproj    # Target: net10.0, references Core + EntityData
├── PokeStream.Core/             # Domain, DTOs, Interfaces, Services (EXISTS)
│   ├── DTOs/
│   │   └── AuthDtos.cs          # RegisterRequest, LoginRequest, AuthResponse, UserInfoDto, ErrorResponse, RefreshTokenRequest
│   ├── Services/
│   │   ├── IAuthService.cs      # Auth interface
│   │   └── AuthService.cs       # JWT + Refresh Tokens + BCrypt implementation
│   ├── Settings/
│   │   └── JwtSettings.cs       # JWT configuration from appsettings
│   └── PokeStream.Core.csproj   # Target: net10.0, references EntityData
├── PokeStream.EntityData/       # EF Core, Models, DbContext, Migrations (EXISTS)
│   ├── Models/
│   │   ├── User.cs              # User with OAuth fields + PasswordHash
│   │   └── UserSession.cs       # Refresh token sessions with device/IP tracking
│   ├── Data/
│   │   └── PokeStreamDbContext.cs  # DbContext with Auth schema config
│   ├── Migrations/
│   │   ├── 20261112210120_InitialAuthSchema.cs    # Initial Users + UserSessions
│   │   └── 20261112211730_AddUpdatedAtToUserSession.cs
│   └── PokeStream.EntityData.csproj  # Target: net10.0, EF Core + SQL Server

PLANNED (not created):
├── PokeStream.Overlay/          # .NET MAUI Desktop App (Overlay)
├── PokeStream.Web/              # Blazor WASM PWA (Viewer App)
├── PokeStream.Bots/             # Shared library for Twitch/YT/Kick bots
└── PokeStream.Infrastructure/   # Additional EF Core, Repositories, Services
```

## Development Commands

```bash
# Solution root
cd Solution/PokeStream

# Build entire solution
dotnet build PokeStream.sln

# Backend API (with JWT Auth + Swagger)
cd PokeStream.API
dotnet restore
dotnet run
# Swagger UI: http://localhost:5130/swagger
# OpenAPI JSON: http://localhost:5130/swagger/v1/swagger.json

# EF Core Migrations (from API project)
dotnet ef migrations add <Name> -o ../PokeStream.EntityData/Data/Migrations --project ../PokeStream.EntityData/PokeStream.EntityData.csproj
dotnet ef database update --project ../PokeStream.EntityData/PokeStream.EntityData.csproj

# Run tests (when added)
dotnet test
```

## Current Auth API Endpoints

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/v1/auth/register` | Register new user | Public |
| POST | `/api/v1/auth/login` | Login (username/email + password) | Public |
| POST | `/api/v1/auth/refresh` | Renew access token using refresh token | Public |
| POST | `/api/v1/auth/logout` | Revoke refresh token | Bearer |
| GET | `/api/v1/auth/me` | Get current user info | Bearer |

## Database Configuration

**Production (Azure SQL Server):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:<server>.database.windows.net,1433;Initial Catalog=PokeStream;Persist Security Info=False;User ID=<user>;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

**Development (Azure SQL Server - separate DB):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:pokeconectserver.database.windows.net,1433;Initial Catalog=PokeApp_dev;Persist Security Info=False;User ID=AdminServerPoke;Password=DevTest@2026;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

**JWT Settings (Development):**
```json
"Jwt": {
  "Key": "PokeStream-Super-Secret-Key-For-Development-Only-Change-In-Production-2026!",
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

## Important Notes
- The MAUI overlay runs on the streamer's machine as an always-on-top transparent window
- Bots connect to multiple platforms simultaneously via shared backend
- Viewer collections persist across platforms (same user on Twitch/YouTube/Kick shares one Pokédex)
- Auto-verification system uses codes/cooldowns to validate captures without manual intervention
- Blazor WASM runs as PWA for offline-capable viewer experience
- **Current Azure SQL**: `pokeconectserver.database.windows.net` with database `PokeApp_dev`
- **Retry logic enabled** in EF Core for Azure SQL transient failures (EnableRetryOnFailure)
- **Password hashing**: BCrypt via ASP.NET Core Identity PasswordHasher
- **Refresh token rotation**: Old token revoked on each refresh for security
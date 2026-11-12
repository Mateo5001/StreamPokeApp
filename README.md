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
| Overlay Desktop | .NET MAUI (Windows/macOS) - *planificado* |
| Web Viewers | Blazor WebAssembly (PWA) - *planificado* |
| **Backend API** | **ASP.NET Core 10, SignalR, EF Core - ✅ IMPLEMENTADO** |
| **Auth** | **JWT Bearer + Refresh Tokens + BCrypt - ✅ IMPLEMENTADO** |
| Database | Azure SQL Server (prod/dev) - ✅ Configurado |
| Bots | TwitchLib, YouTube LiveChat, Kick API - *planificado* |
| Deploy | Docker Compose / Azure Container Apps - *planificado* |
| API Docs | Swagger/OpenAPI (Swashbuckle.AspNetCore 6.6.2) - ✅ Configurado |

---

## 📁 Estructura del proyecto

```
Solution/PokeStream/
├── PokeStream.sln                    # Solution file
├── PokeStream.API/                   # ASP.NET Core API
│   ├── Controllers/
│   │   └── AuthController.cs         # Endpoints: register, login, refresh, logout, me
│   ├── Program.cs                    # JWT Auth + Swagger + EF Core
│   ├── appsettings.json              # Config producción (placeholders)
│   └── PokeStream.API.csproj
├── PokeStream.Core/                  # Dominio, DTOs, Interfaces, Servicios
│   ├── DTOs/
│   │   └── AuthDtos.cs               # Request/Response auth
│   ├── Services/
│   │   ├── IAuthService.cs           # Interfaz
│   │   └── AuthService.cs            # JWT + Refresh + BCrypt
│   ├── Settings/
│   │   └── JwtSettings.cs            # Config JWT
│   └── PokeStream.Core.csproj
├── PokeStream.EntityData/            # EF Core, Modelos, DbContext, Migraciones
│   ├── Models/
│   │   ├── User.cs                   # Usuario + OAuth + PasswordHash
│   │   └── UserSession.cs            # Sesiones refresh token
│   ├── Data/
│   │   └── PokeStreamDbContext.cs    # DbContext Auth
│   ├── Migrations/
│   └── PokeStream.EntityData.csproj

PROYECTOS PLANIFICADOS:
├── PokeStream.Overlay/               # .NET MAUI Desktop App
├── PokeStream.Web/                   # Blazor WASM PWA
├── PokeStream.Bots/                  # Bots Twitch/YouTube/Kick
└── PokeStream.Infrastructure/        # Repositorios, Servicios extra
```

---

## 🚀 Primeros pasos

### Prerrequisitos
- .NET 10 SDK
- Azure SQL Server configurado
- EF Core Tools: `dotnet tool install --global dotnet-ef`

### Backend API

```bash
# Desde la raíz de la solución
cd Solution/PokeStream

# Compilar todo
dotnet build PokeStream.sln

# Ejecutar API
cd PokeStream.API
dotnet restore
dotnet run

# Swagger UI: http://localhost:5130/swagger
```

### Migraciones BD

```bash
# Crear migración
dotnet ef migrations add <Nombre> \
  -o ../PokeStream.EntityData/Data/Migrations \
  --project ../PokeStream.EntityData/PokeStream.EntityData.csproj

# Aplicar a BD
dotnet ef database update \
  --project ../PokeStream.EntityData/PokeStream.EntityData.csproj
```

### Endpoints Auth disponibles

| Método | Endpoint | Descripción | Auth |
|--------|----------|-------------|------|
| POST | `/api/v1/auth/register` | Registrar usuario | Público |
| POST | `/api/v1/auth/login` | Login (user/email + pass) | Público |
| POST | `/api/v1/auth/refresh` | Renovar access token | Público |
| POST | `/api/v1/auth/logout` | Revocar refresh token | Bearer |
| GET | `/api/v1/auth/me` | Info usuario actual | Bearer |

---

## ⚙️ Configuración

La configuración sensible (connection strings, JWT keys, passwords) se gestiona vía:

- **Desarrollo**: `appsettings.Development.json` (gitignored)
- **Producción**: Variables de entorno / Azure Key Vault / User Secrets

> **Nunca** commitear credenciales reales. Usa `appsettings.Example.json` como plantilla.

---

## 🔐 Detalles de implementación Auth

- **Password Hashing**: BCrypt via `Microsoft.AspNetCore.Identity.PasswordHasher<User>`
- **Access Token**: JWT HS256, 15 min expiración, claims: `sub`, `username`, `email`, `displayName`, `jti`, `iat`
- **Refresh Token**: 64 bytes random (Base64), 7 días expiración, almacenado hasheado (SHA256) en BD
- **Rotación**: Cada refresh invalida el token anterior (revocado con razón "Rotado por nuevo token")
- **Resiliencia Azure SQL**: `EnableRetryOnFailure` (5 reintentos, 30s max delay) + `CommandTimeout(60)`

---

## 📋 Estado actual (2026-11-12)

| Componente | Estado | Detalles |
|------------|--------|----------|
| Solution (.sln) | ✅ | Formato clásico compatible VS 2022 |
| PokeStream.API | ✅ | net10.0, JWT Auth, Swagger, Controllers |
| PokeStream.Core | ✅ | net10.0, DTOs, IAuthService, AuthService, JwtSettings |
| PokeStream.EntityData | ✅ | net10.0, User, UserSession, DbContext, 2 Migraciones |
| Azure SQL Database | ✅ | Configurado y migraciones aplicadas |
| Build solution | ✅ | 0 errores, 2 warnings (vulnerabilidades paquetes transitivos) |
| Auth endpoints probados | ✅ | register, login, refresh funcionando |

---

## 🗺 Roadmap próximo

- [ ] Eliminar `WeatherForecastController` y modelo por defecto
- [ ] Implementar OAuth (Twitch, YouTube, Kick, Discord)
- [ ] SignalR Hub para overlay en tiempo real
- [ ] PokeStream.Bots library
- [ ] PokeStream.Overlay (MAUI)
- [ ] PokeStream.Web (Blazor WASM PWA)
- [ ] Tests unitarios e integración
- [ ] Docker / CI/CD

---

## 📄 Licencia

MIT
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
| **Health Checks** | **Liveness/Readiness probes - ✅ IMPLEMENTADO** |
| Database | Azure SQL Server (prod/dev) - ✅ Configurado |
| Bots | TwitchLib, YouTube LiveChat, Kick API - *planificado* |
| Deploy | Docker Compose / Azure Container Apps - *planificado* |
| API Docs | Swagger/OpenAPI (Swashbuckle.AspNetCore 6.6.2) - ✅ Configurado |


### Endpoints Auth disponibles

| Método | Endpoint | Descripción | Auth |
|--------|----------|-------------|------|
| POST | `/api/v1/auth/register` | Registrar usuario | Público |
| POST | `/api/v1/auth/login` | Login (user/email + pass) | Público |
| POST | `/api/v1/auth/refresh` | Renovar access token | Público |
| POST | `/api/v1/auth/logout` | Revocar refresh token | Bearer |
| GET | `/api/v1/auth/me` | Info usuario actual | Bearer |

### Endpoints Health disponibles

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/health/live` | Liveness probe (k8s/container) |
| GET | `/health/ready` | Readiness probe (DB connectivity) |
| GET | `/health` | Detailed health check with DB info |

---

## 📄 Licencia

MIT
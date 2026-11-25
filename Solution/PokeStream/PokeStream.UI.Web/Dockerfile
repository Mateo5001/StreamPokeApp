# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first for restore cache
COPY ["Solution/PokeStream/PokeStream.Core/PokeStream.Core.csproj", "Solution/PokeStream/PokeStream.Core/"]
COPY ["Solution/PokeStream/PokeStream.EntityData/PokeStream.EntityData.csproj", "Solution/PokeStream/PokeStream.EntityData/"]
COPY ["Solution/PokeStream/PokeStream.Resources/PokeStream.Resources.csproj", "Solution/PokeStream/PokeStream.Resources/"]
COPY ["Solution/PokeStream/PokeStream.UI.Web/PokeStream.UI.Web.csproj", "Solution/PokeStream/PokeStream.UI.Web/"]

RUN dotnet restore "Solution/PokeStream/PokeStream.UI.Web/PokeStream.UI.Web.csproj"

# Copy all source code
COPY . .

# Publish (not --no-build: Blazor's static assets are produced during the build)
FROM build AS publish
WORKDIR "/src/Solution/PokeStream/PokeStream.UI.Web"
RUN dotnet publish "PokeStream.UI.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Create non-root user
# -m: ASP.NET Core writes its encryption keys under the home folder
RUN groupadd -r appuser && useradd -r -m -g appuser appuser
USER appuser

# Copy published app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=10s --retries=3 \
  CMD curl -f http://localhost:8080/health/live || exit 1

# Entry point
ENTRYPOINT ["dotnet", "PokeStream.UI.Web.dll"]

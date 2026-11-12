# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files for restore cache
COPY ["Solution/PokeStream/PokeStream.sln", "Solution/PokeStream/"]
COPY ["Solution/PokeStream/PokeStream.Core/PokeStream.Core.csproj", "Solution/PokeStream/PokeStream.Core/"]
COPY ["Solution/PokeStream/PokeStream.EntityData/PokeStream.EntityData.csproj", "Solution/PokeStream/PokeStream.EntityData/"]
COPY ["Solution/PokeStream/PokeStream.API/PokeStream.API.csproj", "Solution/PokeStream/PokeStream.API/"]

# Restore dependencies
RUN dotnet restore "Solution/PokeStream/PokeStream.sln"

# Copy all source code
COPY . .

# Build
WORKDIR "/src/Solution/PokeStream/PokeStream.API"
RUN dotnet build "PokeStream.API.csproj" -c Release -o /app/build --no-restore

# Publish
FROM build AS publish
RUN dotnet publish "PokeStream.API.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Create non-root user
RUN groupadd -r appuser && useradd -r -g appuser appuser
USER appuser

# Copy published app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=10s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Entry point
ENTRYPOINT ["dotnet", "PokeStream.API.dll"]
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeStream.EntityData.Data;

namespace PokeStream.API.Controllers;

/// <summary>
/// Health check endpoints for container orchestration and monitoring
/// </summary>
[ApiController]
[Route("health")]
[Produces("application/json")]
public class HealthController : ControllerBase
{
    private readonly PokeStreamDbContext _dbContext;

    public HealthController(PokeStreamDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Liveness probe - checks if the application is running
    /// </summary>
    [HttpGet("live")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Live()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }

    /// <summary>
    /// Readiness probe - checks if the application is ready to serve traffic
    /// </summary>
    [HttpGet("ready")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> Ready()
    {
        try
        {
            // Check database connectivity
            var canConnect = await _dbContext.Database.CanConnectAsync();

            if (!canConnect)
            {
                return StatusCode(503, new { status = "Unhealthy", reason = "Database connection failed", timestamp = DateTime.UtcNow });
            }

            return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new { status = "Unhealthy", reason = ex.Message, timestamp = DateTime.UtcNow });
        }
    }

    /// <summary>
    /// Detailed health check with database info
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> Health()
    {
        var checks = new List<HealthCheck>();

        // Database check
        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();
            checks.Add(new HealthCheck
            {
                Name = "Database",
                Status = canConnect ? "Healthy" : "Unhealthy",
                Description = canConnect ? "Connected to Azure SQL" : "Cannot connect to database"
            });
        }
        catch (Exception ex)
        {
            checks.Add(new HealthCheck
            {
                Name = "Database",
                Status = "Unhealthy",
                Description = ex.Message
            });
        }

        var allHealthy = checks.All(c => c.Status == "Healthy");

        return allHealthy
            ? Ok(new { status = "Healthy", checks, timestamp = DateTime.UtcNow })
            : StatusCode(503, new { status = "Unhealthy", checks, timestamp = DateTime.UtcNow });
    }
}

public class HealthCheck
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
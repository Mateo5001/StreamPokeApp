using Microsoft.EntityFrameworkCore;
using PokeStream.EntityData.Models;

namespace PokeStream.EntityData.Data;

/// <summary>
/// Entity Framework DbContext for PokéStream application - Auth module only
/// </summary>
public class PokeStreamDbContext : DbContext
{
    public PokeStreamDbContext(DbContextOptions<PokeStreamDbContext> options) : base(options)
    {
    }

    // DbSets for Auth entities only
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserSession> UserSessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUser(modelBuilder);
        ConfigureUserSession(modelBuilder);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();

        entity.ToTable("Users");
        entity.HasKey(u => u.Id);
        entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
        entity.Property(u => u.DisplayName).HasMaxLength(200);
        entity.Property(u => u.Email).HasMaxLength(320);
        entity.Property(u => u.AvatarUrl).HasMaxLength(500);
        entity.Property(u => u.TwitchUserId).HasMaxLength(100);
        entity.Property(u => u.TwitchUsername).HasMaxLength(100);
        entity.Property(u => u.YouTubeChannelId).HasMaxLength(100);
        entity.Property(u => u.YouTubeUsername).HasMaxLength(100);
        entity.Property(u => u.KickUserId).HasMaxLength(100);
        entity.Property(u => u.KickUsername).HasMaxLength(100);
        entity.Property(u => u.DiscordUserId).HasMaxLength(100);
        entity.Property(u => u.DiscordUsername).HasMaxLength(100);
        entity.Property(u => u.TwitchAccessToken).HasMaxLength(1000);
        entity.Property(u => u.TwitchRefreshToken).HasMaxLength(1000);
        entity.Property(u => u.YouTubeAccessToken).HasMaxLength(1000);
        entity.Property(u => u.YouTubeRefreshToken).HasMaxLength(1000);
        entity.Property(u => u.KickAccessToken).HasMaxLength(1000);
        entity.Property(u => u.KickRefreshToken).HasMaxLength(1000);

        // Indexes
        entity.HasIndex(u => u.Username).IsUnique();
        entity.HasIndex(u => u.Email).IsUnique();
        entity.HasIndex(u => u.TwitchUserId).IsUnique();
        entity.HasIndex(u => u.YouTubeChannelId).IsUnique();
        entity.HasIndex(u => u.KickUserId).IsUnique();
        entity.HasIndex(u => u.DiscordUserId).IsUnique();
        entity.HasIndex(u => u.IsActive);

        // Relationships
        entity.HasMany(u => u.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureUserSession(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<UserSession>();

        entity.ToTable("UserSessions");
        entity.HasKey(s => s.Id);
        entity.Property(s => s.RefreshToken).IsRequired().HasMaxLength(500);
        entity.Property(s => s.DeviceInfo).HasMaxLength(200);
        entity.Property(s => s.IpAddress).HasMaxLength(100);
        entity.Property(s => s.UserAgent).HasMaxLength(500);
        entity.Property(s => s.RevokedReason).HasMaxLength(500);
        entity.Property(s => s.UpdatedAt).IsRequired();

        // Indexes
        entity.HasIndex(s => s.UserId);
        entity.HasIndex(s => s.RefreshToken).IsUnique();
        entity.HasIndex(s => s.ExpiresAt);
        entity.HasIndex(s => s.IsActive);
    }
}
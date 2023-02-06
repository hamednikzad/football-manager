using FootballManager.Application.Common.Interfaces;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Infrastructure.Persistence;

public class FootballDbContext : DbContext, IApplicationDbContext
{
    public virtual DbSet<Team> Teams { get; set; } = null!;
    public virtual DbSet<Player> Players { get; set; } = null!;
    public DbSet<PlayerEvent> PlayerEvents { get; set; } = null!;

    public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            entity
                .Ignore(t => t.SquadSize)
                .Ignore(t => t.Foreigners)
                .Ignore(t => t.AverageAge);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            entity
                .HasMany(p => p.PlayersEvents)
                .WithOne(e => e.Player)
                .HasForeignKey(e => e.PlayerId);
        });

        modelBuilder.Entity<PlayerEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}
using FootballManager.Application.Common.Interfaces;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Persist;

public class FootballDbContext : DbContext, IApplicationDbContext
{
    public virtual DbSet<Team> Teams { get; set; } = null!;
    public virtual DbSet<Player> Player { get; set; } = null!;

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
                .WithOne(s => s.Team)
                .HasForeignKey(s => s.TeamId);

            entity
                .Ignore(t => t.SquadSize)
                .Ignore(t => t.Foreigners)
                .Ignore(t => t.AverageAge);
        });

        modelBuilder.Entity<Player>(entity => { entity.HasKey(s => s.Id); });
    }
}
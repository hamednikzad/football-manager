using FootballManager.Application.Common.Interfaces;
using FootballManager.Application.Players;
using FootballManager.Application.Teams;
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
            entity.HasKey(g => g.Id);
            
            entity
                .HasMany(g => g.Players)
                .WithOne(s => s.Team)
                .HasForeignKey(s => s.TeamId);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(s => s.Id);
        });
    }
}
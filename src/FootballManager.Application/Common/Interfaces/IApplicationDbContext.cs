using FootballManager.Application.Players;
using FootballManager.Application.Teams;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Team> Teams { get; }
    DbSet<Player> Player { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
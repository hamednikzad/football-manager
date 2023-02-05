﻿using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Team> Teams { get; }
    DbSet<Player> Player { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FootballManager.Application.Common.Exceptions;
using FootballManager.Application.Common.Interfaces;
using FootballManager.Application.Players.Dto;
using FootballManager.Application.Teams.Dto;

namespace FootballManager.Application.Teams;

public class TeamService : ITeamService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TeamService(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async ValueTask<List<TeamSmallDto>> GetTeams(CancellationToken ct)
    {
        return await _context
            .Teams
            .Include(t => t.Players)
            .ProjectTo<TeamSmallDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
    }

    public async ValueTask<TeamDto> GetById(int teamId, CancellationToken ct)
    {
        var team = await _context
            .Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == teamId, ct);

        if (team is null)
            throw new NotFoundException(nameof(teamId), teamId);

        return _mapper.Map<TeamDto>(team);
    }

    public async ValueTask<TeamDto> GetByName(string name, CancellationToken ct)
    {
        var team = await _context
            .Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Name == name, ct);

        if (team is null)
            throw new NotFoundException(nameof(name), name);

        return _mapper.Map<TeamDto>(team);
    }

    public async ValueTask<List<PlayerSmallDto>> GetTeamPlayers(int teamId, CancellationToken ct)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId, ct);

        if (team is null)
            throw new NotFoundException(nameof(teamId), teamId);

        var players = await _context
            .Players
            .Where(p => p.TeamId == teamId)
            .ProjectTo<PlayerSmallDto>(_mapper.ConfigurationProvider).ToListAsync(ct);
        
        return players;
    }

    public async ValueTask<List<PlayerSmallDto>> GetTeamPlayers(string name, CancellationToken ct)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Name == name, ct);

        if (team is null)
            throw new NotFoundException(nameof(name), name);

        var players = await _context
            .Players
            .Where(p => p.TeamId == team.Id)
            .ProjectTo<PlayerSmallDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);
        
        return players;
    }
}
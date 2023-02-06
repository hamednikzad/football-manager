using AutoMapper;
using AutoMapper.QueryableExtensions;
using FootballManager.Application.Common.Exceptions;
using FootballManager.Application.Common.Interfaces;
using FootballManager.Application.Players.Dto;
using FootballManager.Core.Players;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Application.Players;

public class PlayerService : IPlayerService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PlayerService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async ValueTask<PlayerDto> GetById(int playerId, CancellationToken ct)
    {
        var player = await _context
            .Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(t => t.Id == playerId, ct);

        if (player is null)
            throw new NotFoundException(nameof(playerId), playerId);

        return _mapper.Map<PlayerDto>(player);
    }

    public async ValueTask AddPlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        var player = await _context.Players.FirstOrDefaultAsync(t => t.Id == playerId, ct);

        if (player is null)
            throw new NotFoundException(nameof(playerId), playerId);

        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId, ct);

        if (team is null)
            throw new NotFoundException(nameof(teamId), teamId);

        player.AddToTeam(teamId, DateTime.Now);

        var addEvent = PlayerEvent.Create(playerId, $"{player.FullName} transferred to {team.Name}", DateTime.Now);

        _context.PlayerEvents.Add(addEvent);
        _context.Players.Update(player);

        await _context.SaveChangesAsync(ct);
    }

    public async ValueTask RemovePlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        var player = await _context.Players.FirstOrDefaultAsync(t => t.Id == playerId, ct);

        if (player is null)
            throw new NotFoundException(nameof(playerId), playerId);

        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId, ct);

        if (team is null)
            throw new NotFoundException(nameof(teamId), teamId);

        player.LeaveTeam();

        var addEvent = PlayerEvent.Create(playerId, $"{player.FullName} leaved {team.Name}", DateTime.Now);

        _context.PlayerEvents.Add(addEvent);
        _context.Players.Update(player);

        await _context.SaveChangesAsync(ct);
    }

    public async ValueTask<List<PlayerEventDto>> GetPlayerEvents(int playerId, int take, CancellationToken ct)
    {
        var player = await _context.Players.FirstOrDefaultAsync(t => t.Id == playerId, ct);

        if (player is null)
            throw new NotFoundException(nameof(playerId), playerId);

        var events = await _context
            .PlayerEvents
            .Where(p => p.PlayerId == playerId)
            .Take(take)
            .ProjectTo<PlayerEventDto>(_mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return events;
    }

    private static FootType GetFoot(string input)
    {
        return input.ToLower() switch
        {
            "right" => FootType.Right,
            "r" => FootType.Right,
            "left" => FootType.Left,
            "l" => FootType.Left,
            _ => FootType.Both
        };
    }

    public async ValueTask<int> DefineNewPlayer(DefinePlayerRequest player, CancellationToken ct)
    {
        var newPlayer = Player.Create(player.FullName, player.Age, player.Height, player.Number, GetFoot(player.Foot),
            player.Nationality);

        _context.Players.Add(newPlayer);

        var defineEvent = PlayerEvent.Create(newPlayer.Id, $"{player.FullName} with Age {newPlayer.Age} defined",
            DateTime.Now);
        
        _context.PlayerEvents.Add(defineEvent);

        await _context.SaveChangesAsync(ct);

        return newPlayer.Id;
    }
}
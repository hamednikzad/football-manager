using FootballManager.Application.Players.Dto;

namespace FootballManager.Application.Players;

public interface IPlayerService
{
    ValueTask<PlayerDto> GetById(int playerId, CancellationToken ct);
    ValueTask AddPlayerToTeam(int playerId, int teamId, CancellationToken ct);
    ValueTask RemovePlayerFromTeam(int playerId, int teamId, CancellationToken ct);
    ValueTask<List<PlayerEventDto>> GetPlayerEvents(int playerId, int take, CancellationToken ct);
    ValueTask<int> DefineNewPlayer(DefinePlayerRequest player, CancellationToken ct);
    Task UpdatePlayer(UpdatePlayerRequest player, CancellationToken ct);
}
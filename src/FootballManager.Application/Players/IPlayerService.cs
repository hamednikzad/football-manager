namespace FootballManager.Application.Players;

public interface IPlayerService
{
    ValueTask<PlayerDto> GetById(int playerId, CancellationToken ct);
    ValueTask AddPlayerToTeam(int playerId, CancellationToken ct);
    ValueTask RemovePlayerToTeam(int playerId, CancellationToken ct);
}
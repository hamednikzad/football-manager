using FootballManager.Application.Players;
using FootballManager.Application.Players.Dto;
using FootballManager.Application.Teams.Dto;

namespace FootballManager.Application.Teams;

public interface ITeamService
{
    ValueTask<List<TeamSmallDto>> GetTeams(CancellationToken ct);
    ValueTask<TeamDto> GetById(int teamId, CancellationToken ct);
    ValueTask<TeamDto> GetByName(string name, CancellationToken ct);
    ValueTask<List<PlayerSmallDto>> GetTeamPlayers(int teamId, CancellationToken ct);
    ValueTask<List<PlayerSmallDto>> GetTeamPlayers(string name, CancellationToken ct);
}
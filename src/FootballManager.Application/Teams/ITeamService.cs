using FootballManager.Application.Players;

namespace FootballManager.Application.Teams;

public interface ITeamService
{
    ValueTask<List<TeamSmallDto>> GetTeams(CancellationToken ct);
    ValueTask<TeamDto> GetById(int teamId, CancellationToken ct);
    ValueTask<TeamDto> GetByName(string name, CancellationToken ct);
    ValueTask<List<PlayerSmallDto>> GetTeamPlayers(int teamId, CancellationToken ct);
    ValueTask<List<PlayerSmallDto>> GetTeamPlayers(string teamId, CancellationToken ct);
}
public class TeamService : ITeamService
{
    public async ValueTask<List<TeamSmallDto>> GetTeams(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TeamDto> GetById(int teamId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TeamDto> GetByName(string name, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<PlayerSmallDto>> GetTeamPlayers(int teamId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<PlayerSmallDto>> GetTeamPlayers(string teamId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
using FootballManager.Application.Players;

namespace FootballManager.Application.Teams;

public class Team
{
    public int Id { get; set; }
    public List<Player> Players { get; set; } = null!;
}
using FootballManager.Application.Teams;

namespace FootballManager.Application.Players;

public class Player
{
    public int Id { get; set; }
    
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
}
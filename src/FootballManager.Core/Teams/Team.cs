using FootballManager.Core.Players;

namespace FootballManager.Core.Teams;

public partial class Team
{
    private Team()
    {
    }

    public int Id { get; set; }
    public string Name { get; private set; } = "";
    public string League { get; private set; } = "";
    public string Country { get; private set; } = "";
    
    public int SquadSize => Players.Count;
    public double AverageAge
    {
        get
        {
            if (!Players.Any())
                return 0;
            
            return Math.Round(Players.Average(p => p.Age), 2);
        }
    }

    public int Foreigners => Players.Count(p => p.Nationality != Country);
    public virtual List<Player> Players { get; } = new();
    
    public static Team Create(string name, string league, string country)
    {
        ValidateName(name);
        ValidateLeague(league);
        
        return new Team()
        {
            Name = name,
            League = league,
            Country = country
        };
    }

    public void AddPlayer(Player player)
    {
        if(Players.Contains(player))
            return;

        player.Team = this;
        player.TeamId = Id;
        
        Players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        if(!Players.Contains(player))
            return;

        player.Team = null;
        player.TeamId = null;
        
        Players.Remove(player);
    }
}
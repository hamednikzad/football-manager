using FootballManager.Core.Players;

namespace FootballManager.Core.Teams;

public class Team
{
    private Team()
    {
    }

    public int Id { get; set; }
    public string Name { get; private set; }
    public string League { get; private set; }
    public string Country { get; private set; }
    
    public int SquadSize => Players.Count;
    public double AverageAge => Players.Average(p => p.Age);
    public int Foreigners => Players.Count(p => p.Nationality != Country);
    public virtual List<Player> Players { get; } = new();
    
    public static Team Create(string name, string league, string country)
    {
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
        
        Players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        if(!Players.Contains(player))
            return;
        
        Players.Remove(player);
    }
}
namespace FootballManager.Core.Teams;

public partial class Team
{
    private static void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
        
    }

    private static void ValidateLeague(string league)
    {
        if (string.IsNullOrEmpty(league)) throw new ArgumentNullException(nameof(league));
    }
}
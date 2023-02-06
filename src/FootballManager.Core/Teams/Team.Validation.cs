namespace FootballManager.Core.Teams;

public partial class Team
{
    private static void ValidateName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException(nameof(fullName));
        
    }

    private static void ValidateLeague(string league)
    {
        if (string.IsNullOrEmpty(league)) throw new ArgumentNullException(nameof(league));
    }
}
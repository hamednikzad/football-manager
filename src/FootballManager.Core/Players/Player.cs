using FootballManager.Core.Teams;

namespace FootballManager.Core.Players;

public partial class Player
{
    private Player()
    {
    }

    public int Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public int Age { get; private set; }

    /// <summary>
    /// In CM
    /// </summary>
    public int Height { get; private set; }

    public int? Number { get; private set; }
    public FootType Foot { get; private set; }
    public string Nationality { get; private set; } = null!;
    public DateTime? JoinedDate { get; private set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
    public virtual List<PlayerEvent> PlayersEvents { get; } = new();

    public static Player Create(string fullName, int age, int height, int? number, FootType foot, string nationality)
    {
        ValidateName(fullName);
        ValidateAge(age);
        ValidateHeight(height);
        ValidateNumber(number);

        return new Player()
        {
            FullName = fullName,
            Age = age,
            Height = height,
            Number = number,
            Foot = foot,
            Nationality = nationality
        };
    }

    public void UpdateName(string fullName)
    {
        ValidateName(fullName);

        FullName = fullName;
    }

    public void UpdateAge(int age)
    {
        ValidateAge(age);

        Age = age;
    }

    public void UpdateNumber(int? number)
    {
        ValidateNumber(number);

        Number = number;
    }


    public void AddToTeam(int teamId, DateTime joinDate)
    {
        TeamId = teamId;
        JoinedDate = joinDate;
    }

    public void LeaveTeam()
    {
        TeamId = null;
        JoinedDate = null;
    }
}
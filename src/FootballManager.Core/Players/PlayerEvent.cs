namespace FootballManager.Core.Players;

public class PlayerEvent
{
    public int Id { get; private set; }
    public DateTime EventDate { get; private init; }
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    public string Message { get; private init; } = "";

    private PlayerEvent()
    {
    }

    public static PlayerEvent Create(int playerId, string message, DateTime date)
    {
        return new PlayerEvent()
        {
            PlayerId = playerId,
            Message = message,
            EventDate = date
        };
    }
}
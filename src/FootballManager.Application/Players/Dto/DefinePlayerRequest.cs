namespace FootballManager.Application.Players.Dto;

public record DefinePlayerRequest
{
    public string FullName { get; init; } = "";
    public int Age { get; init; }
    public int Height { get; init; }
    public int? Number { get; init; }
    public string Nationality { get; init; } = "";
    public string Foot { get; init; } = "Right";
}
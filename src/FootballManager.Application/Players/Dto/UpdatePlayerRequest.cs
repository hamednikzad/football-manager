namespace FootballManager.Application.Players.Dto;

public record UpdatePlayerRequest
{
    public int PlayerId { get; init; }
    public string? FullName { get; init; }
    public int? Age { get; init; }
    public int? Number { get; init; }
}
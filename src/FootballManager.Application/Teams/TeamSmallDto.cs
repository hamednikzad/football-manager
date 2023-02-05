namespace FootballManager.Application.Teams;

public record TeamSmallDto(int Id, string Name)
{
    public int Id { get; set; } = Id;
    public string Name { get; set; } = Name;
}
public record TeamDto(int Id, string Name)
{
    public int Id { get; set; } = Id;
    public string Name { get; set; } = Name;
}
using AutoMapper;
using FootballManager.Application.Common.Mappings;
using FootballManager.Application.Players.Dto;
using FootballManager.Core.Teams;

namespace FootballManager.Application.Teams.Dto;

public record TeamDto : IMapFrom<Team>
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string League { get; set; } = "";
    public string Country { get; set; } = "";
    public int SquadSize { get; set; }
    public double AverageAge { get; set; }
    public int Foreigners { get; set; }
    
    public List<PlayerSmallDto> Players { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Team, TeamDto>();
    }
}
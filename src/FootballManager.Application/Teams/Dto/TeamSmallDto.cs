using AutoMapper;
using FootballManager.Application.Common.Mappings;
using FootballManager.Core.Teams;

namespace FootballManager.Application.Teams.Dto;

public class TeamSmallDto : IMapFrom<Team>
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string League { get; set; } = "";
    public string Country { get; set; } = "";
    public int SquadSize { get; set; }
    public double AverageAge { get; set; }
    public int Foreigners { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Team, TeamSmallDto>();
    }
}
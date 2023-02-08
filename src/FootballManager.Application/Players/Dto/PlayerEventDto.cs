using AutoMapper;
using FootballManager.Application.Common.Mappings;
using FootballManager.Core.Players;

namespace FootballManager.Application.Players.Dto;

public class PlayerEventDto : IMapFrom<PlayerEvent>
{
    public DateTime EventDate { get; init; }
    public int PlayerId { get; init; }
    public string Message { get; init; } = "";
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerEvent, PlayerEventDto>();
    }
}
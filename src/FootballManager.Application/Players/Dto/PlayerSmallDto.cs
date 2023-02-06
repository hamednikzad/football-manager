using AutoMapper;
using FootballManager.Application.Common.Mappings;
using FootballManager.Core.Players;

namespace FootballManager.Application.Players.Dto;

public class PlayerSmallDto : IMapFrom<Player>
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public int Age { get; set; }

    /// <summary>
    /// In CM
    /// </summary>
    public string Height { get; set; } = "";

    public int? Number { get; set; }
    public string Foot { get; set; } = "";
    public string Nationality { get; set; } = "";
    public DateTime? JoinedDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, PlayerSmallDto>()
            .ForMember(d => d.Height,
                opt =>
                    opt.MapFrom(s => s.Height + "cm"));
    }
}
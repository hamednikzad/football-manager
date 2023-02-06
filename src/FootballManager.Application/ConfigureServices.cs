using System.Reflection;
using FootballManager.Application.Players;
using FootballManager.Application.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace FootballManager.Application;

public static class ConfigureServices
{
    public static void Config(this IServiceCollection services)
    {        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<ITeamService, TeamService>();
    }
}
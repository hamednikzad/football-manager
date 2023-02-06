using FootballManager.Application.Players.Dto;
using FootballManager.Application.Teams;
using FootballManager.Application.Teams.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FootballManager.Api.Controllers;

/// <summary>
/// Teams Actions
/// </summary>
[ApiController]
[Route("teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="teamService"></param>
    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    #region Queries
    
    /// <summary>
    /// Get teams in brief
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<TeamSmallDto>>> GetTeams(CancellationToken ct)
    {
        var teams = await _teamService.GetTeams(ct);
        
        return Ok(teams);
    }

    /// <summary>
    /// Get team by Id
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{teamId:int}")]
    public async Task<ActionResult<TeamDto>> GetTeam(int teamId, CancellationToken ct)
    {
        var team = await _teamService.GetById(teamId, ct);
        
        return Ok(team);
    }

    /// <summary>
    /// Get team by name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    public async Task<ActionResult<TeamDto>> GetTeam(string name, CancellationToken ct)
    {
        var team = await _teamService.GetByName(name, ct);
        
        return Ok(team);
    }

    /// <summary>
    /// Get players of a team by team Id
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{teamId:int}/players")]
    public async Task<ActionResult<List<PlayerSmallDto>>> GetTeamPlayers(int teamId, CancellationToken ct)
    {
        var players = await _teamService.GetTeamPlayers(teamId, ct);
        
        return Ok(players);
    }

    /// <summary>
    /// Get players of a team by team name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("name/{name}/players")]
    public async Task<ActionResult<List<PlayerSmallDto>>> GetTeamPlayers(string name, CancellationToken ct)
    {
        var players = await _teamService.GetTeamPlayers(name, ct);
        
        return Ok(players);
    }
    
    #endregion
}
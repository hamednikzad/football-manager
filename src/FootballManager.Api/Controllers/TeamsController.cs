using FootballManager.Application.Players;
using FootballManager.Application.Teams;
using Microsoft.AspNetCore.Mvc;

namespace FootballManager.Api.Controllers;

[ApiController]
[Route("teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    #region Queries
    
    [HttpGet]
    public async Task<ActionResult<List<TeamSmallDto>>> GetTeams(CancellationToken ct)
    {
        var teams = await _teamService.GetTeams(ct);
        
        return Ok(teams);
    }

    [HttpGet("{teamId:int}")]
    public async Task<ActionResult<TeamDto>> GetTeam(int teamId, CancellationToken ct)
    {
        var team = await _teamService.GetById(teamId, ct);
        
        return Ok(team);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<TeamDto>> GetTeam(string name, CancellationToken ct)
    {
        var team = await _teamService.GetByName(name, ct);
        
        return Ok(team);
    }

    [HttpGet("{teamId:int}/players")]
    public async Task<ActionResult<List<PlayerSmallDto>>> GetTeamPlayers(int teamId, CancellationToken ct)
    {
        var players = await _teamService.GetTeamPlayers(teamId, ct);
        
        return Ok(players);
    }

    [HttpGet("name/{name}/players")]
    public async Task<ActionResult<List<PlayerSmallDto>>> GetTeamPlayers(string name, CancellationToken ct)
    {
        var players = await _teamService.GetTeamPlayers(name, ct);
        
        return Ok(players);
    }
    
    #endregion
}
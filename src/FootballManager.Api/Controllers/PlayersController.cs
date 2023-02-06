using FootballManager.Application.Players;
using Microsoft.AspNetCore.Mvc;

namespace FootballManager.Api.Controllers;

[ApiController]
[Route("players")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    #region Queries
    
    [HttpGet("{playerId:int}")]
    public async Task<ActionResult<PlayerDto>> GetPlayer(int playerId, CancellationToken ct)
    {
        var player = await _playerService.GetById(playerId, ct);
        
        return Ok(player);
    }
    
    [HttpGet("{playerId:int}/events")]
    public async Task<ActionResult<List<PlayerEventDto>>> GetPlayerEvents(int playerId, CancellationToken ct, int take = 20)
    {
        var events = await _playerService.GetPlayerEvents(playerId, take, ct);
        
        return Ok(events);
    }
    
    #endregion

    #region Commands

    [HttpPost("")]
    public async Task<ActionResult<int>> CreatePlayer([FromBody] DefinePlayerRequest player, CancellationToken ct)
    {
        var playerId = await _playerService.DefineNewPlayer(player, ct);

        return Ok(playerId);
    }

    [HttpPut("{playerId:int}/team/{teamId:int}")]
    public async Task<ActionResult<PlayerDto>> AddPlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        await _playerService.AddPlayerToTeam(playerId, teamId, ct);
        
        return Ok();
    }

    [HttpDelete("{playerId:int}/team/{teamId:int}")]
    public async Task<ActionResult<PlayerDto>> RemovePlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        await _playerService.RemovePlayerToTeam(playerId, teamId, ct);
        
        return Ok();
    }

    #endregion
}
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
    
    #endregion

    #region Commands

    [HttpPut("{playerId:int}/team/{teamId:int}")]
    public async Task<ActionResult<PlayerDto>> AddPlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        await _playerService.AddPlayerToTeam(playerId, ct);
        
        return Ok();
    }

    [HttpDelete("{playerId:int}/team/{teamId:int}")]
    public async Task<ActionResult<PlayerDto>> RemovePlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        await _playerService.RemovePlayerToTeam(playerId, ct);
        
        return Ok();
    }

    #endregion
}
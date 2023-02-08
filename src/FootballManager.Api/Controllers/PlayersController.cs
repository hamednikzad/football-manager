using FootballManager.Application.Players;
using FootballManager.Application.Players.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FootballManager.Api.Controllers;

/// <summary>
/// Players actions
/// </summary>
[ApiController]
[Route("players")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerService"></param>
    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    #region Queries
    
    /// <summary>
    /// Get player by Id
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{playerId:int}")]
    public async Task<ActionResult<PlayerDto>> GetPlayer(int playerId, CancellationToken ct)
    {
        var player = await _playerService.GetById(playerId, ct);
        
        return Ok(player);
    }
    
    /// <summary>
    /// Get events for Player
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="ct"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [HttpGet("{playerId:int}/events")]
    public async Task<ActionResult<List<PlayerEventDto>>> GetPlayerEvents(int playerId, CancellationToken ct, int take = 20)
    {
        var events = await _playerService.GetPlayerEvents(playerId, take, ct);
        
        return Ok(events);
    }
    
    #endregion

    #region Commands

    /// <summary>
    /// Create player
    /// </summary>
    /// <param name="player"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("")]
    public async Task<ActionResult<int>> CreatePlayer([FromBody] DefinePlayerRequest player, CancellationToken ct)
    {
        var playerId = await _playerService.DefineNewPlayer(player, ct);

        return Ok(playerId);
    }

    /// <summary>
    /// Update player's properties
    /// </summary>
    /// <param name="player"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("")]
    public async Task<ActionResult<int>> UpdatePlayer([FromBody] UpdatePlayerRequest player, CancellationToken ct)
    {
        await _playerService.UpdatePlayer(player, ct);

        return Ok();
    }

    /// <summary>
    /// Add player to Team
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="teamId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("{playerId:int}/team/{teamId:int}")]
    public async Task<ActionResult<PlayerDto>> AddPlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        await _playerService.AddPlayerToTeam(playerId, teamId, ct);
        
        return Ok();
    }

    /// <summary>
    /// Remove player from Team
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="teamId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{playerId:int}/team/{teamId:int}")]
    public async Task<ActionResult<PlayerDto>> RemovePlayerToTeam(int playerId, int teamId, CancellationToken ct)
    {
        await _playerService.RemovePlayerFromTeam(playerId, teamId, ct);
        
        return Ok();
    }

    #endregion
}
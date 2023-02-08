using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FootballManager.Application.Common.Exceptions;
using FootballManager.Application.Common.Interfaces;
using FootballManager.Application.Common.Mappings;
using FootballManager.Application.Players;
using FootballManager.Application.Players.Dto;
using FootballManager.Application.Teams;
using FootballManager.Application.Teams.Dto;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using FootballManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FootballManager.Application.Tests.Players;

public class PlayerServiceTest
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public PlayerServiceTest()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    private static IApplicationDbContext CreateDbContext()
    {
        var random = new Random((int)DateTime.Now.Ticks);
        var name = $"Db{random.Next()}";
        var dbContextOptions = new DbContextOptionsBuilder<FootballDbContext>()
            .UseInMemoryDatabase(databaseName: name)
            .Options;

        var dbContext = new FootballDbContext(dbContextOptions);
        return dbContext;
    }

    [Fact]
    public void Should_Have_Valid_Configuration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    #region Query

    [Fact]
    public async Task GetById_Should_Return_Correct_Team()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;

        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");

        dbContext.Players.Add(player1);

        await dbContext.SaveChangesAsync(ct);

        var sut = new PlayerService(dbContext, _mapper);

        //Act
        var actual = await sut.GetById(player1.Id, ct);

        var expected = _mapper.Map<PlayerDto>(player1);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetPlayerEvents_Should_Return_LatestEventsForPlayer()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;

        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");

        dbContext.Players.Add(player1);
        dbContext.Teams.Add(team);

        await dbContext.SaveChangesAsync(ct);

        var sut = new PlayerService(dbContext, _mapper);
        await sut.AddPlayerToTeam(player1.Id, team.Id, ct);
        await dbContext.SaveChangesAsync(ct);

        //Act
        var actual = await sut.GetPlayerEvents(player1.Id, 10, ct);

        //Assert
        actual.Count.Should().Be(1);
    }

    #endregion

    #region Acions

    [Fact]
    public async Task AddPlayerToTeam_Should_IncreaseTeamPlayers()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;

        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");

        dbContext.Players.Add(player1);
        dbContext.Teams.Add(team);

        await dbContext.SaveChangesAsync(ct);

        //Act
        var sut = new PlayerService(dbContext, _mapper);
        await sut.AddPlayerToTeam(player1.Id, team.Id, ct);
        await dbContext.SaveChangesAsync(ct);

        var actual = dbContext.Teams.Include(t => t.Players).First(t => t.Id == team.Id);

        //Assert
        actual.Players.Count.Should().Be(1);
    }

    [Fact]
    public async Task RemovePlayerFromTeam_Should_DecreaseTeamPlayers()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;

        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");

        dbContext.Players.Add(player1);
        dbContext.Teams.Add(team);

        await dbContext.SaveChangesAsync(ct);

        //Act
        var sut = new PlayerService(dbContext, _mapper);
        await sut.RemovePlayerFromTeam(player1.Id, team.Id, ct);
        await dbContext.SaveChangesAsync(ct);

        var actual = dbContext.Teams.Include(t => t.Players).First(t => t.Id == team.Id);

        //Assert
        actual.Players.Count.Should().Be(0);
    }

    [Fact]
    public async Task DefineNewPlayer_Should_AddPlayerToDb()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;

        var newPlayerRequest = new DefinePlayerRequest()
        {
            Age = 35,
            Foot = "Right",
            Height = 180,
            Number = 10,
            FullName = "Player 1"
        };

        //Act
        var sut = new PlayerService(dbContext, _mapper);
        await sut.DefineNewPlayer(newPlayerRequest, ct);
        
        var actual = await dbContext.Players.FirstAsync(p => p.FullName == newPlayerRequest.FullName, ct);

        //Assert
        actual.FullName.Should().Be(newPlayerRequest.FullName);
        actual.Age.Should().Be(newPlayerRequest.Age);
        actual.Number.Should().Be(newPlayerRequest.Number);
    }

    [Fact]
    public async Task UpdatePlayer_Should_UpdatePlayerProperties()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;

        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");

        dbContext.Players.Add(player1);

        await dbContext.SaveChangesAsync(ct);

        //Act
        var sut = new PlayerService(dbContext, _mapper);

        var request = new UpdatePlayerRequest
        {
            PlayerId = player1.Id,
            Age = 25,
            Number = 20,
            FullName = "Player 2"
        };
        await sut.UpdatePlayer(request, ct);
        
        var actual = await dbContext.Players.FirstAsync(p => p.Id == request.PlayerId, ct);

        //Assert
        actual.FullName.Should().Be(request.FullName);
        actual.Age.Should().Be(request.Age);
        actual.Number.Should().Be(request.Number);
    }

    #endregion
}
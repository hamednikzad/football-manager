using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FootballManager.Application.Common.Exceptions;
using FootballManager.Application.Common.Interfaces;
using FootballManager.Application.Common.Mappings;
using FootballManager.Application.Players.Dto;
using FootballManager.Application.Teams;
using FootballManager.Application.Teams.Dto;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using FootballManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FootballManager.Application.Tests.Teams;

public class TeamServiceTest
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public TeamServiceTest()
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
        
        var team = Team.Create("Juventus", "Serie A", "Italy");
        
        dbContext.Teams.Add(team);

        await dbContext.SaveChangesAsync(ct);

        var sut = new TeamService(dbContext, _mapper);

        //Act
        var actual = await sut.GetById(team.Id, ct);

        var expected = _mapper.Map<TeamDto>(team);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public async Task GetByName_Should_Return_Correct_Team()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;
        
        var team = Team.Create("Juventus", "Serie A", "Italy");
        
        dbContext.Teams.Add(team);

        await dbContext.SaveChangesAsync(ct);

        var sut = new TeamService(dbContext, _mapper);

        //Act
        var actual = await sut.GetByName(team.Name, ct);

        var expected = _mapper.Map<TeamDto>(team);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public async Task GetById_Should_Throw_NotFoundException_On_Invalid_Id()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;
        
        var team = Team.Create("Juventus", "Serie A", "Italy");
        
        dbContext.Teams.Add(team);

        await dbContext.SaveChangesAsync(ct);

        var sut = new TeamService(dbContext, _mapper);

        //Act
        var act = async () => await sut.GetById(10, ct);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetTeams_Should_Return_Correct_Teams()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;
        
        var team1 = Team.Create("Team 1", "Serie A", "Italy");
        var team2 = Team.Create("Team 2", "Serie A", "Italy");
        
        dbContext.Teams.Add(team1);
        dbContext.Teams.Add(team2);

        await dbContext.SaveChangesAsync(ct);

        var sut = new TeamService(dbContext, _mapper);

        //Act
        var actual = await sut.GetTeams(ct);

        var expected = _mapper.Map<List<TeamSmallDto>>(new List<Team>()
        {
            team1, team2
        });

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetTeamPlayersById_Should_Return_Correct_Players()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;
        
        var team1 = Team.Create("Team 1", "Serie A", "Italy");
        
        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");
        var player2 = Player.Create("Player 2", 35, 180, 10, FootType.Both, "Italy");
        
        team1.AddPlayer(player1);
        team1.AddPlayer(player2);
        
        dbContext.Teams.Add(team1);
        dbContext.Players.Add(player1);
        dbContext.Players.Add(player2);

        await dbContext.SaveChangesAsync(ct);

        var sut = new TeamService(dbContext, _mapper);

        //Act
        var actual = await sut.GetTeamPlayers(team1.Id, ct);

        var expected = _mapper.Map<List<PlayerSmallDto>>(new List<Player>()
        {
            player1, player2
        });

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetTeamPlayersByName_Should_Return_Correct_Players()
    {
        //Arrange
        var dbContext = CreateDbContext();
        var ct = CancellationToken.None;
        
        var team1 = Team.Create("Team 1", "Serie A", "Italy");
        
        var player1 = Player.Create("Player 1", 35, 180, 10, FootType.Both, "Italy");
        var player2 = Player.Create("Player 2", 35, 180, 10, FootType.Both, "Italy");
        
        team1.AddPlayer(player1);
        team1.AddPlayer(player2);
        
        dbContext.Teams.Add(team1);
        dbContext.Players.Add(player1);
        dbContext.Players.Add(player2);

        await dbContext.SaveChangesAsync(ct);

        var sut = new TeamService(dbContext, _mapper);

        //Act
        var actual = await sut.GetTeamPlayers(team1.Name, ct);

        var expected = _mapper.Map<List<PlayerSmallDto>>(new List<Player>
        {
            player1, player2
        });

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    #endregion
    
}
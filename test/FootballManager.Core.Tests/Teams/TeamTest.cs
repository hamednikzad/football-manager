using System;
using System.Linq;
using FluentAssertions;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using Xunit;

namespace FootballManager.Core.Tests.Teams;

public class TeamTest
{
    #region Creation

    [Fact]
    public void CreateTeam_Should_Throw_ArgumentNullException_On_Null_Name()
    {
        Action act = () => Team.Create(null!, "Serie A", "Italy");

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreateTeam_Should_Throw_ArgumentNullException_On_Null_League()
    {
        Action act = () => Team.Create("Juventus", null!, "Italy");

        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region TeamModifications

    [Fact]
    public void AddPlayer_Should_ShouldAddPlayerToTeam()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");

        //Act
        team.AddPlayer(player);

        //Assert
        team.Players.First().Should().Be(player);
    }

    [Fact]
    public void RemovePlayer_Should_RemovePlayerFromTeam()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");

        team.AddPlayer(player);
        //Act
        team.RemovePlayer(player);

        //Assert
        team.Players.Count.Should().Be(0);
    }

    #endregion

    #region ComputedProperties

    [Fact]
    public void SquadSize_Should_BeEquivalentOfPlayersCount()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");

        //Act
        team.AddPlayer(player);

        //Assert
        team.SquadSize.Should().Be(team.Players.Count);
    }

    [Fact]
    public void AverageAge_Should_ShouldIncreaseSquadSize()
    {
        //Arrange
        var player1 = Player.Create("Player 1", 20, 180, 10, FootType.Both, "Italy");
        var player2 = Player.Create("Player 2", 30, 180, 10, FootType.Both, "Italy");
        
        var team = Team.Create("Juventus", "Serie A", "Italy");

        //Act
        team.AddPlayer(player1);
        team.AddPlayer(player2);

        //Assert
        team.AverageAge.Should().Be(25);
    }

    [Fact]
    public void Foreigners_Should_BeCountOfForeignPlayersOfTeam()
    {
        //Arrange
        var player1 = Player.Create("Player 1", 20, 180, 10, FootType.Both, "Spain");
        var player2 = Player.Create("Player 2", 30, 180, 10, FootType.Both, "Netherlands");
        
        var team = Team.Create("Juventus", "Serie A", "Italy");

        //Act
        team.AddPlayer(player1);
        team.AddPlayer(player2);

        //Assert
        team.Foreigners.Should().Be(2);
    }
    
    #endregion
}
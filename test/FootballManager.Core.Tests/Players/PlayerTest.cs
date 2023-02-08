using System;
using FluentAssertions;
using FootballManager.Core.Players;
using FootballManager.Core.Teams;
using Xunit;

namespace FootballManager.Core.Tests.Players;

public class PlayerTest
{
    #region Creation

    [Fact]
    public void CreatePlayer_Should_Throw_ArgumentNullException_On_Null_Name()
    {
        Action act = () => Player.Create(null!, 35, 180, 10, FootType.Both, "Italy");

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreatePlayer_Should_Throw_ArgumentOutOfRangeException_On_Negative_Age()
    {
        Action act = () => Player.Create("Hamed", -35, 180, 10, FootType.Both, "Italy");

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CreatePlayer_Should_Throw_ArgumentOutOfRangeException_On_NotValid_Height()
    {
        Action act = () => Player.Create("Hamed", 35, 0, 10, FootType.Both, "Italy");

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CreatePlayer_Should_Throw_ArgumentOutOfRangeException_On_Negative_Number()
    {
        Action act = () => Player.Create("Hamed", 35, 180, -10, FootType.Both, "Italy");

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    #endregion
    
    #region Modification
    
    [Fact]
    public void UpdateName_Should_ChangeNameOfPlayer()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");

        //Act
        const string expected = "Vahid";
        player.UpdateName(expected);

        //Assert
        player.FullName.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void UpdateAge_Should_ChangeAgeOfPlayer()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");

        //Act
        const int expected = 37;
        player.UpdateAge(37);

        //Assert
        player.Age.Should().Be(expected);
    }
    
    [Fact]
    public void UpdateNumber_Should_ChangePlayerNumber()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");

        //Act
        const int expected = 20;
        player.UpdateNumber(20);

        //Assert
        player.Number.Should().Be(expected);
    }
    
    #endregion

    #region TeamModifications

    [Fact]
    public void AddToTeam_Should_SetPlayersTeam()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");
        var team = Team.Create("Juventus", "Serie A", "Italy");
        
        //Act
        player.AddToTeam(team.Id, DateTime.Now);

        //Assert
        player.TeamId.Should().Be(team.Id);
    }

    [Fact]
    public void LeaveTeam_Should_SetPlayersTeamIdNull()
    {
        //Arrange
        var player = Player.Create("Hamed", 35, 180, 10, FootType.Both, "Italy");
        
        //Act
        player.LeaveTeam();

        //Assert
        player.TeamId.Should().BeNull();
    }
    
    #endregion
}
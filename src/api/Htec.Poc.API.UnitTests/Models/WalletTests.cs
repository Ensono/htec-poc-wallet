using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using Htec.Poc.API.Models.Responses;

namespace Htec.Poc.API.UnitTests.Models;

public class WalletTests
{
    [Fact]
    public void Id_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Id")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void Id_Should_ReturnGuid()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Id")
            .Should()
            .Return<Guid?>();
    }

    [Fact]
    public void Name_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Name")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void Name_Should_ReturnString()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Name")
            .Should()
            .Return<string>();
    }

    [Fact]
    public void Points_Should_ReturnInteger()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Points")
            .Should()
            .Return<int>();
    }

    [Fact]
    public void Points_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Points")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void Enabled_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Enabled")
            .Should()
            .BeDecoratedWith<RequiredAttribute>();
    }

    [Fact]
    public void Enabled_Should_ReturnBool()
    {
        // Arrange
        // Act
        // Assert
        typeof(Wallet)
            .Properties()
            .First(x => x.Name == "Enabled")
            .Should()
            .Return<bool?>();
    }
}

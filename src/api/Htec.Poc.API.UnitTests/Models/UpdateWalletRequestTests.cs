using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using Htec.Poc.API.Models.Requests;

namespace Htec.Poc.API.UnitTests.Models;

public class UpdateWalletRequestTests
{
    [Fact]
    public void Name_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateWalletRequest)
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
        typeof(UpdateWalletRequest)
            .Properties()
            .First(x => x.Name == "Name")
            .Should()
            .Return<string>();
    }

    [Fact]
    public void Description_Should_ReturnString()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateWalletRequest)
            .Properties()
            .First(x => x.Name == "Description")
            .Should()
            .Return<string>();
    }

    [Fact]
    public void Enabled_Should_BeDecoratedWith_RequiredAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(UpdateWalletRequest)
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
        typeof(UpdateWalletRequest)
            .Properties()
            .First(x => x.Name == "Enabled")
            .Should()
            .Return<bool>();
    }
}

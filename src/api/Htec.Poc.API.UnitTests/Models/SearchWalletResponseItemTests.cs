using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using Htec.Poc.API.Models.Responses;

namespace Htec.Poc.API.UnitTests.Models;

public class SearchWalletResponseItemTests
{
    [Fact]
    public void Id_Should_ReturnGuid()
    {
        // Arrange
        // Act
        // Assert
        typeof(SearchWalletResponseItem)
            .Properties()
            .First(x => x.Name == "Id")
            .Should()
            .Return<Guid>();
    }

    [Fact]
    public void Name_Should_ReturnString()
    {
        // Arrange
        // Act
        // Assert
        typeof(SearchWalletResponseItem)
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
        typeof(SearchWalletResponseItem)
            .Properties()
            .First(x => x.Name == "Description")
            .Should()
            .Return<string>();
    }

    [Fact]
    public void Enabled_Should_ReturnBool()
    {
        // Arrange
        // Act
        // Assert
        typeof(SearchWalletResponseItem)
            .Properties()
            .First(x => x.Name == "Enabled")
            .Should()
            .Return<bool>();
    }
}

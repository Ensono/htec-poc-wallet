using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using HTEC.POC.Domain.Entities;

namespace HTEC.POC.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class WalletItemTests
{
    [Theory, AutoData]
    public void Constructor(
        Guid categoryId,
        string name,
        string description,
        double price,
        bool available)
    {
        // Arrange
        // Act
        var walletItem = new WalletItem(categoryId, name, description, price, available);

        // Assert
        walletItem.Id.Should().Be(categoryId);
        walletItem.Name.Should().Be(name);
        walletItem.Description.Should().Be(description);
        walletItem.Price.Should().Be(price);
        walletItem.Available.Should().Be(available);
    }
}

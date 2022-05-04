using System;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Htec.Poc.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class WalletTests
{
    [Theory, AutoData]
    public void Constructor(
        string name,
        bool enabled,
        int points)
    {
        // Arrange
        // Act
        var wallet = new Wallet(Guid.Empty, name, enabled, points);

        // Assert
        wallet.Name.Should().Be(name);
        wallet.Enabled.Should().Be(enabled);
        wallet.Points.Should().Be(points);
    }
    
    [Theory, AutoData]
    public void Update(
        Wallet wallet,
        string name,
        bool enabled,
        int points)
    {
        // Arrange
        // Act
        wallet.Update(name, enabled, points);

        // Assert
        wallet.Name.Should().Be(name);
        wallet.Enabled.Should().Be(enabled);
        wallet.Points.Should().Be(points);
    }    
}

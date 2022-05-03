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
        string description,
        bool enabled)
    {
        // Arrange
        // Act
        var wallet = new Wallet(Guid.Empty, name, Guid.Empty, description, enabled);

        // Assert
        wallet.Name.Should().Be(name);
        wallet.Description.Should().Be(description);
        wallet.Enabled.Should().Be(enabled);
    }
    
    [Theory, AutoData]
    public void Update(
        Wallet wallet,
        string name,
        string description,
        bool enabled)
    {
        // Arrange
        // Act
        wallet.Update(name, description, enabled);

        // Assert
        wallet.Name.Should().Be(name);
        wallet.Description.Should().Be(description);
        wallet.Enabled.Should().Be(enabled);
    }    
}

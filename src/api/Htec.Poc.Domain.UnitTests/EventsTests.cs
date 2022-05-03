using Amido.Stacks.Domain.Events;
using FluentAssertions;
using Xunit;
using Htec.Poc.Domain.Events;

namespace Htec.Poc.Domain.UnitTests;

public class EventsTests
{
    [Fact]
    public void WalletChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void WalletCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletCreated).Should().Implement<IDomainEvent>();
    }
}

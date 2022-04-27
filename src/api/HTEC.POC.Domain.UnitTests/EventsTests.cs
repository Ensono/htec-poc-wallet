using Amido.Stacks.Domain.Events;
using FluentAssertions;
using Xunit;
using HTEC.POC.Domain.Events;

namespace HTEC.POC.Domain.UnitTests;

public class EventsTests
{
    [Fact]
    public void CategoryChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void CategoryCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void CategoryRemoved()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryRemoved).Should().Implement<IDomainEvent>();
    }

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

    [Fact]
    public void WalletItemChanged()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletItemChanged).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void WalletItemCreated()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletItemCreated).Should().Implement<IDomainEvent>();
    }

    [Fact]
    public void WalletItemRemoved()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletItemRemoved).Should().Implement<IDomainEvent>();
    }
}

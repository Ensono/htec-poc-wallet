using System;
using Amido.Stacks.Core.Exceptions;
using FluentAssertions;
using Xunit;
using HTEC.POC.Domain.WalletAggregateRoot.Exceptions;

namespace HTEC.POC.Domain.UnitTests;

public class ExceptionsTests
{
    [Fact]
    public void CategoryAlreadyExistsException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryAlreadyExistsException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void CategoryAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => CategoryAlreadyExistsException.Raise(Guid.Empty, "testCategoryName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<CategoryAlreadyExistsException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void CategoryAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => CategoryAlreadyExistsException.Raise(Guid.Empty, "testCategoryName", null);

        // Assert
        act
            .Should()
            .Throw<CategoryAlreadyExistsException>()
            .WithMessage("A category with name 'testCategoryName' already exists in the wallet '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void CategoryDoesNotExistException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(CategoryDoesNotExistException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void CategoryDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => CategoryDoesNotExistException.Raise(Guid.Empty, Guid.Empty, expectedMessage);

        // Assert
        act
            .Should()
            .Throw<CategoryDoesNotExistException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void CategoryDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => CategoryDoesNotExistException.Raise(Guid.Empty, Guid.Empty, null);

        // Assert
        act
            .Should()
            .Throw<CategoryDoesNotExistException>()
            .WithMessage("A category with id '00000000-0000-0000-0000-000000000000' does not exist in the wallet '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void WalletItemAlreadyExistsException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletItemAlreadyExistsException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void WalletItemAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => WalletItemAlreadyExistsException.Raise(Guid.Empty, "walletItemName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<WalletItemAlreadyExistsException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void WalletItemAlreadyExistsException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => WalletItemAlreadyExistsException.Raise(Guid.Empty, "walletItemName", null);

        // Assert
        act
            .Should()
            .Throw<WalletItemAlreadyExistsException>()
            .WithMessage("The item walletItemName already exist in the category '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void WalletItemDoesNotExistException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletItemDoesNotExistException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void WalletItemDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => WalletItemDoesNotExistException.Raise(Guid.Empty, Guid.Empty, expectedMessage);

        // Assert
        act
            .Should()
            .Throw<WalletItemDoesNotExistException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void WalletItemDoesNotExistException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => WalletItemDoesNotExistException.Raise(Guid.Empty, Guid.Empty, null);

        // Assert
        act
            .Should()
            .Throw<WalletItemDoesNotExistException>()
            .WithMessage("The item 00000000-0000-0000-0000-000000000000 does not exist in the category '00000000-0000-0000-0000-000000000000'.");
    }

    [Fact]
    public void WalletItemPriceMustNotBeZeroException_DerivesFrom_DomainExceptionBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(WalletItemPriceMustNotBeZeroException).Should().BeDerivedFrom<DomainExceptionBase>();
    }

    [Fact]
    public void WalletItemPriceMustNotBeZeroException_Throws_CorrespondingExceptionWithExpectedMessage()
    {
        // Arrange
        var expectedMessage = "This is the expected message";
        // Act
        Action act = () => WalletItemPriceMustNotBeZeroException.Raise("itemName", expectedMessage);

        // Assert
        act
            .Should()
            .Throw<WalletItemPriceMustNotBeZeroException>()
            .WithMessage(expectedMessage);
    }

    [Fact]
    public void WalletItemPriceMustNotBeZeroException_Throws_CorrespondingExceptionWithExpectedMessage_WhenMessageIsNull()
    {
        // Arrange
        // Act
        Action act = () => WalletItemPriceMustNotBeZeroException.Raise("itemName", null);

        // Assert
        act
            .Should()
            .Throw<WalletItemPriceMustNotBeZeroException>()
            .WithMessage("The price for the item itemName had price as zero. A price must be provided.");
    }
}

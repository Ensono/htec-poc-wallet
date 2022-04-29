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
    
    [Theory, AutoData]
    public void AddCategory(
        Wallet wallet,
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        // Act
        wallet.AddCategory(categoryId, name, description);

        // Assert
        wallet.Categories.Last().Id.Should().Be(categoryId);
        wallet.Categories.Last().Name.Should().Be(name);
        wallet.Categories.Last().Description.Should().Be(description);
    }

    [Theory, AutoData]
    public void UpdateCategory(
        Wallet wallet,
        Guid categoryId,
        string name,
        string description,
        string updatedName,
        string updatedDescription)
    {
        // Arrange
        wallet.AddCategory(categoryId, name, description);

        // Act
        wallet.UpdateCategory(categoryId, updatedName, updatedDescription);

        // Assert
        wallet.Categories.Last().Id.Should().Be(categoryId);
        wallet.Categories.Last().Name.Should().Be(updatedName);
        wallet.Categories.Last().Description.Should().Be(updatedDescription);
    }

    [Theory, AutoData]
    public void RemoveCategory(
        Wallet wallet,
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        wallet.AddCategory(categoryId, name, description);
        var categoriesLength = wallet.Categories.Count;
        
        // Assert
        wallet.Categories.Should().Contain(category => category.Id == categoryId);

        // Act
        wallet.RemoveCategory(categoryId);

        // Assert
        wallet.Categories.Should().NotContain(category => category.Id == categoryId);
        wallet.Categories.Count.Should().Be(categoriesLength - 1);
    }
    
    [Theory, AutoData]
    public void AddWalletItem(
        Wallet wallet,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid walletItemId,
        string walletItemName,
        string walletItemDescription,
        double walletItemPrice,
        bool walletItemAvailable)
    {
        // Arrange
        wallet.AddCategory(categoryId, categoryName, categoryDescription);

        // Act
        wallet.AddWalletItem(categoryId, walletItemId, walletItemName, walletItemDescription, walletItemPrice, walletItemAvailable);
        
        // Assert
        wallet
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(walletItem => walletItem.Id == walletItemId && 
                                 walletItem.Name == walletItemName &&
                                 walletItem.Description == walletItemDescription &&
                                 walletItem.Price == walletItemPrice &&
                                 walletItem.Available == walletItemAvailable);
    }

    [Theory, AutoData]
    public void UpdateWalletItem(
        Wallet wallet,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid walletItemId,
        string walletItemName,
        string walletItemDescription,
        double walletItemPrice,
        bool walletItemAvailable,
        string updatedWalletItemName,
        string updatedWalletItemDescription,
        double updatedWalletItemPrice,
        bool updatedWalletItemAvailable)
    {
        // Arrange
        wallet.AddCategory(categoryId, categoryName, categoryDescription);
        wallet.AddWalletItem(categoryId, walletItemId, walletItemName, walletItemDescription, walletItemPrice, walletItemAvailable);

        // Act
        wallet.UpdateWalletItem(categoryId, walletItemId, updatedWalletItemName, updatedWalletItemDescription, updatedWalletItemPrice, updatedWalletItemAvailable);
        
        // Assert
        wallet
            .Categories
            .Last()
            .Items
            .Should()
            .NotContain(walletItem => walletItem.Id == walletItemId && 
                                    walletItem.Name == walletItemName && 
                                    walletItem.Description == walletItemDescription && 
                                    walletItem.Price == walletItemPrice && 
                                    walletItem.Available == walletItemAvailable);
        
        wallet
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(walletItem => walletItem.Id == walletItemId && 
                                 walletItem.Name == updatedWalletItemName && 
                                 walletItem.Description == updatedWalletItemDescription && 
                                 walletItem.Price == updatedWalletItemPrice && 
                                 walletItem.Available == updatedWalletItemAvailable);
    }

    [Theory, AutoData]
    public void RemoveWalletItem(
        Wallet wallet,
        Guid categoryId,
        string categoryName,
        string categoryDescription,
        Guid walletItemId,
        string walletItemName,
        string walletItemDescription,
        double walletItemPrice,
        bool walletItemAvailable)
    {
        // Arrange
        wallet.AddCategory(categoryId, categoryName, categoryDescription);
        wallet.AddWalletItem(categoryId, walletItemId, walletItemName, walletItemDescription, walletItemPrice, walletItemAvailable);

        // Assert
        wallet
            .Categories
            .Last()
            .Items
            .Should()
            .Contain(walletItem => walletItem.Id == walletItemId &&
                                 walletItem.Name == walletItemName &&
                                 walletItem.Description == walletItemDescription &&
                                 walletItem.Price == walletItemPrice &&
                                 walletItem.Available == walletItemAvailable);

        // Act
        wallet.RemoveWalletItem(categoryId, walletItemId);

        // Assert
        wallet
            .Categories
            .Last()
            .Items
            .Should()
            .NotContain(walletItem => walletItem.Id == walletItemId &&
                                    walletItem.Name == walletItemName &&
                                    walletItem.Description == walletItemDescription &&
                                    walletItem.Price == walletItemPrice &&
                                    walletItem.Available == walletItemAvailable);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.DynamoDB.Abstractions;
using FluentAssertions;
using NSubstitute;
using Xunit;
using HTEC.POC.Application.Integration;
using HTEC.POC.Domain;
using HTEC.POC.Domain.Entities;
using HTEC.POC.Infrastructure.Repositories;

namespace HTEC.POC.Infrastructure.UnitTests;

public class DynamoDbWalletRepositoryTests
{
    private readonly DynamoDbWalletRepository repository;
    private readonly IDynamoDbObjectStorage<Wallet> fakeWalletRepository;
    private Wallet wallet;

    public DynamoDbWalletRepositoryTests()
    {
        fakeWalletRepository = SetupFakeWalletRepository();
        repository = new DynamoDbWalletRepository(fakeWalletRepository);
    }

    [Fact]
    public void DynamoDbWalletRepository_Should_ImplementIWalletRepository()
    {
        // Arrange
        // Act
        // Assert
        typeof(DynamoDbWalletRepository).Should().Implement<IWalletRepository>();
    }

    [Fact]
    public async Task GetByIdAsync()
    {
        // Arrange
        // Act
        var result = await repository.GetByIdAsync(Guid.Empty);

        // Assert
        await fakeWalletRepository.Received().GetByIdAsync(Arg.Any<string>());

        result.Should().BeOfType<Wallet>();
        result.Should().Be(wallet);
    }

    [Fact]
    public async Task SaveAsync()
    {
        // Arrange
        // Act
        var result = await repository.SaveAsync(wallet);

        // Assert
        await fakeWalletRepository.Received().SaveAsync(wallet.Id.ToString(), wallet);

        result.Should().Be(true);
    }

    [Fact]
    public async Task DeleteAsync()
    {
        // Arrange
        // Act
        var result = await repository.DeleteAsync(Guid.Empty);

        // Assert
        await fakeWalletRepository.Received().DeleteAsync(Arg.Any<string>());

        result.Should().Be(true);
    }

    private IDynamoDbObjectStorage<Wallet> SetupFakeWalletRepository()
    {
        var walletRepository = Substitute.For<IDynamoDbObjectStorage<Wallet>>();
        wallet = new Wallet(Guid.Empty, "testName", Guid.Empty, "testDescription", true, new List<Category>());
        var fakeTypeResponse = new OperationResult<Wallet>(true, wallet, new Dictionary<string, string>());
        var fakeNonTypeResponse = new OperationResult(true, new Dictionary<string, string>());

        walletRepository.GetByIdAsync(Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        walletRepository.SaveAsync(wallet.Id.ToString(), wallet)
            .Returns(Task.FromResult(fakeTypeResponse));
        walletRepository.DeleteAsync(Arg.Any<string>())
            .Returns(Task.FromResult(fakeNonTypeResponse));

        return walletRepository;
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.Data.Documents.Abstractions;
using FluentAssertions;
using NSubstitute;
using Xunit;
using HTEC.POC.Application.Integration;
using HTEC.POC.Domain;
using HTEC.POC.Domain.Entities;
using HTEC.POC.Infrastructure.Repositories;

namespace HTEC.POC.Infrastructure.UnitTests;

public class CosmosDbWalletRepositoryTests
{
    private readonly CosmosDbWalletRepository repository;
    private readonly IDocumentStorage<Wallet> fakeWalletRepository;
    private Wallet wallet;

    public CosmosDbWalletRepositoryTests()
    {
        fakeWalletRepository = SetupFakeWalletRepository();
        repository = new CosmosDbWalletRepository(fakeWalletRepository);
    }

    [Fact]
    public void CosmosDbWalletRepository_Should_ImplementIWalletRepository()
    {
        // Arrange
        // Act
        // Assert
        typeof(CosmosDbWalletRepository).Should().Implement<IWalletRepository>();
    }

    [Fact]
    public async Task GetByIdAsync()
    {
        // Arrange
        // Act
        var result = await repository.GetByIdAsync(Guid.Empty);

        // Assert
        await fakeWalletRepository.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>());

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
        await fakeWalletRepository.Received().SaveAsync(Arg.Any<string>(), Arg.Any<string>(), wallet, Arg.Any<string>());

        result.Should().Be(true);
    }

    [Fact]
    public async Task DeleteAsync()
    {
        // Arrange
        // Act
        var result = await repository.DeleteAsync(Guid.Empty);

        // Assert
        await fakeWalletRepository.Received().DeleteAsync(Arg.Any<string>(), Arg.Any<string>());

        result.Should().Be(true);
    }

    private IDocumentStorage<Wallet> SetupFakeWalletRepository()
    {
        var walletRepository = Substitute.For<IDocumentStorage<Domain.Wallet>>();
        wallet = new Wallet(Guid.Empty, "testName", Guid.Empty, "testDescription", true, new List<Category>());
        var fakeTypeResponse = new OperationResult<Wallet>(true, wallet, new Dictionary<string, string>());
        var fakeNonTypeResponse = new OperationResult(true, new Dictionary<string, string>());

        walletRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        walletRepository.SaveAsync(Arg.Any<string>(), Arg.Any<string>(), wallet, Arg.Any<string>())
            .Returns(Task.FromResult(fakeTypeResponse));
        walletRepository.DeleteAsync(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromResult(fakeNonTypeResponse));

        return walletRepository;
    }
}

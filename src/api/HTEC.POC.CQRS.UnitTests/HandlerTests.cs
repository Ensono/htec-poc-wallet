using System;
using System.Collections.Generic;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Amido.Stacks.Data.Documents;
using Amido.Stacks.Data.Documents.Abstractions;
using AutoFixture;
using AutoFixture.Xunit2;
using NSubstitute;
using Shouldly;
using Xunit;
using HTEC.POC.Application.CommandHandlers;
using HTEC.POC.Application.Integration;
using HTEC.POC.Application.QueryHandlers;
using HTEC.POC.Common.Exceptions;
using HTEC.POC.CQRS.Commands;
using HTEC.POC.CQRS.Queries.GetWalletById;
using HTEC.POC.CQRS.Queries.SearchWallet;
using HTEC.POC.Domain.WalletAggregateRoot.Exceptions;
using Query = HTEC.POC.CQRS.Queries;

namespace HTEC.POC.CQRS.UnitTests;

/// <summary>
/// Series of tests for command handlers
/// </summary>
[Trait("TestType", "UnitTests")]
public class HandlerTests
{
    private Fixture fixture;
    private IWalletRepository walletRepo;
    private IApplicationEventPublisher eventPublisher;
    private IDocumentSearch<Domain.Wallet> storage;

    public HandlerTests()
    {
        fixture = new Fixture();
        fixture.Register(() => Substitute.For<IOperationContext>());
        fixture.Register(() => Substitute.For<IWalletRepository>());
        fixture.Register(() => Substitute.For<IApplicationEventPublisher>());
        fixture.Register(() => Substitute.For<IDocumentSearch<Domain.Wallet>>());

        walletRepo = fixture.Create<IWalletRepository>();
        eventPublisher = fixture.Create<IApplicationEventPublisher>();
        storage = fixture.Create<IDocumentSearch<Domain.Wallet>>();
    }

    #region CREATE

    [Theory, AutoData]
    public async void CreateWalletCommandHandler_HandleAsync(CreateWallet command)
    {
        // Arrange
        var handler = new CreateWalletCommandHandler(walletRepo, eventPublisher);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        await walletRepo.Received(1).SaveAsync(Arg.Any<Domain.Wallet>());
        await eventPublisher.Received(1).PublishAsync(Arg.Any<IApplicationEvent>());

        result.ShouldBeOfType<Guid>();
    }

    [Theory, AutoData]
    public async void CreateCategoryCommandHandler_HandleAsync(Domain.Wallet wallet, CreateCategory command)
    {
        // Arrange
        var handler = new CreateCategoryCommandHandler(walletRepo, eventPublisher);

        // Act
        var result = await handler.HandleCommandAsync(wallet, command);

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    [Theory, AutoData]
    public async void CreateWalletItemCommandHandler_HandleAsync(Domain.Wallet wallet, CreateWalletItem command)
    {
        // Arrange
        var handler = new CreateWalletItemCommandHandler(walletRepo, eventPublisher);
        command.CategoryId = wallet.Categories[0].Id;

        // Act
        var result = await handler.HandleCommandAsync(wallet, command);

        // Assert
        result.ShouldBeOfType<Guid>();
    }

    #endregion

    #region DELETE

    [Theory, AutoData]
    public async void DeleteWalletCommandHandler_HandleAsync(Domain.Wallet wallet, DeleteWallet command)
    {
        // Arrange
        walletRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(wallet);
        walletRepo.DeleteAsync(Arg.Any<Guid>()).Returns(true);

        var handler = new DeleteWalletCommandHandler(walletRepo, eventPublisher);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        await walletRepo.Received(1).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(1).PublishAsync(Arg.Any<IApplicationEvent>());

        result.ShouldBeOfType<bool>();
        result.ShouldBeTrue();
    }

    [Theory, AutoData]
    public async void DeleteWalletCommandHandler_HandleAsync_WalletMissing_ShouldThrow(DeleteWallet command)
    {
        // Arrange
        var handler = fixture.Create<DeleteWalletCommandHandler>();

        // Act
        // Assert
        await handler.HandleAsync(command).ShouldThrowAsync<WalletDoesNotExistException>();
        await walletRepo.Received(0).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    [Theory, AutoData]
    public async void DeleteWalletCommandHandler_HandleAsync_NotSuccessful_ShouldThrow(Domain.Wallet wallet, DeleteWallet command)
    {
        // Arrange
        walletRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(wallet);
        walletRepo.DeleteAsync(Arg.Any<Guid>()).Returns(false);
        var handler = new DeleteWalletCommandHandler(walletRepo, eventPublisher);

        // Act
        // Assert
        await handler.HandleAsync(command).ShouldThrowAsync<OperationFailedException>();
        await walletRepo.Received(1).DeleteAsync(Arg.Any<Guid>());
        await eventPublisher.Received(0).PublishAsync(Arg.Any<IApplicationEvent>());
    }

    #endregion

    #region UPDATE

    [Theory, AutoData]
    public async void UpdateWalletCommandHandler_HandleAsync(Domain.Wallet wallet, UpdateWallet command)
    {
        // Arrange
        var handler = new UpdateWalletCommandHandler(walletRepo, eventPublisher);

        // Act
        var result = await handler.HandleCommandAsync(wallet, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateCategoryCommandHandler_HandleAsync(Domain.Wallet wallet, UpdateCategory command)
    {
        // Arrange
        var handler = new UpdateCategoryCommandHandler(walletRepo, eventPublisher);
        command.CategoryId = wallet.Categories[0].Id;

        // Act
        var result = await handler.HandleCommandAsync(wallet, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateWalletItemCommandHandler_HandleAsync(Domain.Wallet wallet, UpdateWalletItem command)
    {
        // Arrange
        var handler = new UpdateWalletItemCommandHandler(walletRepo, eventPublisher);
        command.CategoryId = wallet.Categories[0].Id;
        command.WalletItemId = wallet.Categories[0].Items[0].Id;


        // Act
        var result = await handler.HandleCommandAsync(wallet, command);

        // Assert
        result.ShouldBeOfType<bool>();
        result.ShouldBe(true);
    }

    [Theory, AutoData]
    public async void UpdateCategoryCommandHandler_HandleAsync_NoCategory_ShouldThrow(Domain.Wallet wallet, UpdateCategory command)
    {
        // Arrange
        var handler = new UpdateCategoryCommandHandler(walletRepo, eventPublisher);

        // Act
        // Assert
        await Should.ThrowAsync<CategoryDoesNotExistException>(async () => await handler.HandleCommandAsync(wallet, command));
    }

    [Theory, AutoData]
    public async void UpdateWalletItemCommandHandler_HandleAsync_NoWalletItem_ShouldThrow(Domain.Wallet wallet, UpdateWalletItem command)
    {
        // Arrange
        var handler = new UpdateWalletItemCommandHandler(walletRepo, eventPublisher);
        command.CategoryId = wallet.Categories[0].Id;

        // Act
        // Assert
        await Should.ThrowAsync<WalletItemDoesNotExistException>(async () => await handler.HandleCommandAsync(wallet, command));
    }

    #endregion

    #region QUERIES

    [Theory, AutoData]
    public async void GetWalletByIdQueryHandler_ExecuteAsync(Domain.Wallet wallet, GetWalletById criteria)
    {
        // Arrange
        walletRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(wallet);
        var handler = new GetWalletByIdQueryHandler(walletRepo);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await walletRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
        result.ShouldNotBeNull();
        result.ShouldBeOfType<Wallet>();
    }

    [Theory, AutoData]
    public async void GetWalletByIdQueryHandler_ExecuteAsync_NoWallet_ReturnNull(GetWalletById criteria)
    {
        // Arrange
        var handler = new GetWalletByIdQueryHandler(walletRepo);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await walletRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
        result.ShouldBeNull();
    }

    [Theory, AutoData]
    public async void SearchWalletQueryHandler_ExecuteAsync(SearchWallet criteria, OperationResult<IEnumerable<Domain.Wallet>> operationResult)
    {
        // Arrange
        storage.Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Wallet, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>())
        .Returns(operationResult);

        var handler = new SearchWalletQueryHandler(storage);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await storage.Received(1).Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Wallet, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>());

        result.ShouldBeOfType<SearchWalletResult>();
    }

    [Fact]
    public async void SearchWalletQueryHandler_ExecuteAsync_NoCriteria_ShouldThrow()
    {
        // Arrange
        var handler = new SearchWalletQueryHandler(storage);

        // Act
        // Assert
        await Should.ThrowAsync<ArgumentException>(async () => await handler.ExecuteAsync(null));
    }

    [Theory, AutoData]
    public async void SearchWalletQueryHandler_ExecuteAsync_NotSuccessful(SearchWallet criteria)
    {
        // Arrange
        var operationResult = new OperationResult<IEnumerable<Domain.Wallet>>(false, null, null);

        storage.Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Wallet, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>())
        .Returns(operationResult);

        var handler = new SearchWalletQueryHandler(storage);

        // Act
        var result = await handler.ExecuteAsync(criteria);

        // Assert
        await storage.Received(1).Search(
            Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Wallet, bool>>>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>());

        result.ShouldBeOfType<SearchWalletResult>();
        result.Results.ShouldBeNull();
    }

    #endregion
}

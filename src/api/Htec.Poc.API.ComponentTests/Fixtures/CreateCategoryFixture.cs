using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Amido.Stacks.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using Htec.Poc.API.Authentication;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.API.Models.Responses;
using Htec.Poc.Application.Integration;

namespace Htec.Poc.API.ComponentTests.Fixtures;

public class CreateCategoryFixture : ApiClientFixture
{
    readonly Domain.Wallet existingWallet;
    readonly Guid userRestaurantId = Guid.Parse("2AA18D86-1A4C-4305-95A7-912C7C0FC5E1");
    readonly CreateCategoryRequest newCategory;

    IWalletRepository repository;
    IApplicationEventPublisher applicationEventPublisher;

    public CreateCategoryFixture(Domain.Wallet wallet, CreateCategoryRequest newCategory, IOptions<JwtBearerAuthenticationConfiguration> jwtBearerAuthenticationOptions)
        : base(jwtBearerAuthenticationOptions)
    {
        this.existingWallet = wallet;
        this.newCategory = newCategory;
    }

    protected override void RegisterDependencies(IServiceCollection collection)
    {
        base.RegisterDependencies(collection);

        // Mocked external dependencies, the setup should
        // come later according to each scenario
        repository = Substitute.For<IWalletRepository>();
        applicationEventPublisher = Substitute.For<IApplicationEventPublisher>();

        collection.AddTransient(IoC => repository);
        collection.AddTransient(IoC => applicationEventPublisher);
    }

    /****** GIVEN ******************************************************/

    internal void GivenAnExistingWallet()
    {
        repository.GetByIdAsync(id: Arg.Is<Guid>(id => id == existingWallet.Id))
            .Returns(existingWallet);

        repository.SaveAsync(entity: Arg.Is<Domain.Wallet>(e => e.Id == existingWallet.Id))
            .Returns(true);
    }

    internal void GivenAWalletDoesNotExist()
    {
        repository.GetByIdAsync(id: Arg.Any<Guid>())
            .Returns((Domain.Wallet)null);
    }

    internal void GivenTheWalletBelongsToUserRestaurant()
    {
        existingWallet.With(m => m.TenantId, userRestaurantId);
    }

    internal void GivenTheWalletDoesNotBelongToUserRestaurant()
    {
        existingWallet.With(m => m.TenantId, Guid.NewGuid());
    }

    internal void GivenTheCategoryDoesNotExist()
    {
        if (existingWallet == null || existingWallet.Categories == null)
            return;

        //Ensure in the future wallet is not created with categories
        for (int i = 0; i < existingWallet.Categories.Count(); i++)
        {
            existingWallet.RemoveCategory(existingWallet.Categories.First().Id);
        }
        existingWallet.ClearEvents();
    }

    internal void GivenTheCategoryAlreadyExist()
    {
        existingWallet.AddCategory(Guid.NewGuid(), newCategory.Name, "Some description");
        existingWallet.ClearEvents();
    }

    /****** WHEN ******************************************************/

    internal async Task WhenTheCategoryIsSubmitted()
    {
        await CreateCategory(existingWallet.Id, newCategory);
    }

    /****** THEN ******************************************************/

    internal async Task ThenTheCategoryIsAddedToWallet()
    {
        var resourceCreated = await GetResponseObject<ResourceCreatedResponse>();
        resourceCreated.ShouldNotBeNull();

        var category = existingWallet.Categories.SingleOrDefault(c => c.Name == newCategory.Name);

        category.ShouldNotBeNull();
        category.Id.ShouldBe(resourceCreated.Id);
        category.Name.ShouldBe(newCategory.Name);
        category.Description.ShouldBe(newCategory.Description);
    }

    internal void ThenWalletIsLoadedFromStorage()
    {
        repository.Received(1).GetByIdAsync(Arg.Is<Guid>(id => id == existingWallet.Id));
    }

    internal void ThenTheWalletShouldBePersisted()
    {
        repository.Received(1).SaveAsync(Arg.Is<Domain.Wallet>(wallet => wallet.Id == existingWallet.Id));
    }

    internal void ThenTheWalletShouldNotBePersisted()
    {
        repository.DidNotReceive().SaveAsync(Arg.Any<Domain.Wallet>());
    }

    internal void ThenAWalletUpdatedEventIsRaised()
    {
        applicationEventPublisher.Received(1).PublishAsync(Arg.Any<WalletUpdatedEvent>());
    }

    internal void ThenAWalletUpdatedEventIsNotRaised()
    {
        applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<WalletCreatedEvent>());
    }

    internal void ThenACategoryCreatedEventIsRaised()
    {
        applicationEventPublisher.Received(1).PublishAsync(Arg.Any<CategoryCreatedEvent>());
    }

    internal void ThenACategoryCreatedEventIsNotRaised()
    {
        applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<CategoryCreatedEvent>());
    }
}

﻿using System;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Htec.Poc.Application.CQRS.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Htec.Poc.API.Authentication;
using Htec.Poc.API.Models.Requests;
using Htec.Poc.Application.Integration;

namespace Htec.Poc.API.ComponentTests.Fixtures;

public class CreateWalletFixture : ApiClientFixture
{
    readonly CreateWalletRequest newWallet;
    IWalletRepository repository;
    IApplicationEventPublisher applicationEventPublisher;

    public CreateWalletFixture(CreateWalletRequest newWallet, IOptions<JwtBearerAuthenticationConfiguration> jwtBearerAuthenticationOptions)
        : base(jwtBearerAuthenticationOptions)
    {
        this.newWallet = newWallet;
    }

    protected override void RegisterDependencies(IServiceCollection collection)
    {
        base.RegisterDependencies(collection);

        // Mocked external dependencies, the setup should
        // come later according to the scenarios
        repository = Substitute.For<IWalletRepository>();
        applicationEventPublisher = Substitute.For<IApplicationEventPublisher>();

        collection.AddTransient(IoC => repository);
        collection.AddTransient(IoC => applicationEventPublisher);
    }


    /****** GIVEN ******************************************************/

    internal void GivenAValidWallet()
    {
        // Don't need to do anything here assuming the
        // newWallet auto generated by AutoFixture is valid
    }

    internal void GivenAInvalidWallet()
    {
        newWallet.Name = null;
    }


    internal void GivenAWalletDoesNotExist()
    {
        repository.GetByIdAsync(id: Arg.Any<Guid>())
            .Returns((Domain.Wallet)null);
    }


    /****** WHEN ******************************************************/

    internal async Task WhenTheWalletCreationIsSubmitted()
    {
        await CreateWallet(newWallet);
    }

    /****** THEN ******************************************************/

    internal void ThenGetWalletByIdIsCalled()
    {
        repository.Received(1).GetByIdAsync(Arg.Any<Guid>());
    }
    internal void ThenTheWalletIsSubmittedToDatabase()
    {
        repository.Received(1).SaveAsync(Arg.Is<Domain.Wallet>(wallet => wallet.Name == newWallet.Name));
    }

    internal void ThenTheWalletIsNotSubmittedToDatabase()
    {
        repository.DidNotReceive().SaveAsync(Arg.Any<Domain.Wallet>());
    }

    internal void ThenAWalletCreatedEventIsRaised()
    {
        applicationEventPublisher.Received(1).PublishAsync(Arg.Any<WalletCreatedEvent>());
    }

    internal void ThenAWalletCreatedEventIsNotRaised()
    {
        applicationEventPublisher.DidNotReceive().PublishAsync(Arg.Any<WalletCreatedEvent>());
    }
}

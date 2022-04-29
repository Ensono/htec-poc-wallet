using System;
using System.Collections.Generic;
using Htec.Poc.API.FunctionalTests.Models;

namespace Htec.Poc.API.FunctionalTests.Builders;

public class WalletBuilder : IBuilder<Wallet>
{
    private Wallet wallet;

    public WalletBuilder()
    {
        wallet = new Wallet();
    }


    public WalletBuilder SetDefaultValues(string name)
    {
        var categoryBuilder = new CategoryBuilder();

        wallet.id = Guid.NewGuid().ToString();
        wallet.name = name;
        wallet.description = "Default test wallet description";
        wallet.enabled = true;
        wallet.categories = new List<Category>()
        {
            categoryBuilder.SetDefaultValues("Burgers")
                .Build()
        };

        return this;
    }

    public WalletBuilder WithId(Guid id)
    {
        wallet.id = id.ToString();
        return this;
    }

    public WalletBuilder WithName(string name)
    {
        wallet.name = name;
        return this;
    }

    public WalletBuilder WithDescription(string description)
    {
        wallet.description = description;
        return this;
    }

    public WalletBuilder WithNoCategories()
    {
        wallet.categories = new List<Category>();
        return this;
    }

    public WalletBuilder WithCategories(List<Category> categories)
    {
        wallet.categories = categories;
        return this;
    }

    public WalletBuilder SetEnabled(bool enabled)
    {
        wallet.enabled = enabled;
        return this;
    }

    public Wallet Build()
    {
        return wallet;
    }
}
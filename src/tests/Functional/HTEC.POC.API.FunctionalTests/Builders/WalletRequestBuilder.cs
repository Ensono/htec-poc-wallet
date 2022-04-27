using HTEC.POC.API.FunctionalTests.Models;

namespace HTEC.POC.API.FunctionalTests.Builders;

public class WalletRequestBuilder : IBuilder<WalletRequest>
{
    private WalletRequest wallet;

    public WalletRequestBuilder()
    {
        wallet = new WalletRequest();
    }

    public WalletRequestBuilder SetDefaultValues(string name)
    {
        wallet.name = name;
        wallet.description = "Updated wallet description";
        wallet.enabled = true;
        return this;
    }

    public WalletRequestBuilder WithName(string name)
    {
        wallet.name = name;
        return this;
    }

    public WalletRequestBuilder WithDescription(string description)
    {
        wallet.description = description;
        return this;
    }

    public WalletRequestBuilder SetEnabled(bool enabled)
    {
        wallet.enabled = enabled;
        return this;
    }

    public WalletRequest Build()
    {
        return wallet;
    }
}
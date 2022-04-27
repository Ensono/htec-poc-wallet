using HTEC.POC.API.FunctionalTests.Models;

namespace HTEC.POC.API.FunctionalTests.Builders;

public class WalletItemBuilder : IBuilder<WalletItemRequest>
{
    private readonly WalletItemRequest walletItem;

    public WalletItemBuilder()
    {
        walletItem = new WalletItemRequest();
    }

    public WalletItemBuilder WithName(string name)
    {
        walletItem.name = name;
        return this;
    }

    public WalletItemBuilder WithDescription(string description)
    {
        walletItem.description = description;
        return this;
    }

    public WalletItemBuilder WithPrice(double price)
    {
        walletItem.price = price;
        return this;
    }

    public WalletItemBuilder WithAvailablity(bool available)
    {
        walletItem.available = available;
        return this;
    }

    public WalletItemRequest Build()
    {
        return walletItem;
    }
        
    public WalletItemBuilder SetDefaultValues(string name)
    {
        walletItem.name = name;
        walletItem.description = "Item description";
        walletItem.price = 12.50;
        walletItem.available = true;
        return this;
    }
}
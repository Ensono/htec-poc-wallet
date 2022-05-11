namespace Htec.Poc.Application.CQRS.Enums;

public enum EventCode
{
    // Wallet operations
    WalletCreated = 101,
    WalletUpdated = 102,
    WalletDeleted = 103,

    // CosmosDB change feed operations
    EntityUpdated = 999
}

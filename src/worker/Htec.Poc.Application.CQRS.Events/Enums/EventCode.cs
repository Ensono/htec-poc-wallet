namespace Htec.Poc.Application.CQRS.Events.Enums;

public enum EventCode
{
    // Wallet operations
    WalletCreated = 101,
    WalletUpdated = 102,
    WalletDeleted = 103,

    // Categories Operations
    CategoryCreated = 201,
    CategoryUpdated = 202,
    CategoryDeleted = 203,

    // Items Operations
    WalletItemCreated = 301,
    WalletItemUpdated = 302,
    WalletItemDeleted = 303,

    // CosmosDB change feed operations
    EntityUpdated = 999
}

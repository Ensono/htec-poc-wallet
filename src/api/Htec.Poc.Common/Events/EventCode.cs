namespace Htec.Poc.Common.Events;

public enum EventCode
{
    // Wallet operations
    WalletCreated = 101,
    WalletUpdated = 102,
    WalletDeleted = 103,

    //GetWallet = 104,
    //SearchWallet = 110,

    CosmosDbChangeFeedEvent = 999
}

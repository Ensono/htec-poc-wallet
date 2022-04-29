namespace Htec.Poc.Common.Operations;

public enum OperationCode
{
    // Wallet operations
    CreateWallet = 101,
    UpdateWallet = 102,
    DeleteWallet = 103,
    GetWalletById = 104,
    SearchWallet = 110,

    // Categories Operations
    CreateCategory = 201,
    UpdateCategory = 202,
    DeleteCategory = 203,

    // Items Operations
    CreateWalletItem = 301,
    UpdateWalletItem = 302,
    DeleteWalletItem = 303
}

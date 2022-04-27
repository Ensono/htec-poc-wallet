using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.WalletAggregateRoot.Exceptions;

[Serializable]
public class WalletItemDoesNotExistException : DomainExceptionBase
{
    private WalletItemDoesNotExistException(
        string message
    ) : base(message)
    {
    }


    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.WalletItemDoesNotExist;

    public static void Raise(Guid categoryId, Guid walletItemId, string message)
    {
        var exception = new WalletItemDoesNotExistException(
            message ?? $"The item {walletItemId} does not exist in the category '{categoryId}'."
        );

        exception.Data["CategoryId"] = categoryId;
        exception.Data["WalletItemId"] = walletItemId;

        throw exception;
    }

    public static void Raise(Guid categoryId, Guid walletItemId)
    {
        Raise(categoryId, walletItemId, null);
    }
}

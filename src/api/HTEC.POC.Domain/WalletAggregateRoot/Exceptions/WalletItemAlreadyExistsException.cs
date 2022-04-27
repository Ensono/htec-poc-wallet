using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.WalletAggregateRoot.Exceptions;

[Serializable]
public class WalletItemAlreadyExistsException : DomainExceptionBase
{
    private WalletItemAlreadyExistsException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.WalletItemAlreadyExists;


    public static void Raise(Guid categoryId, string walletItemName, string message)
    {
        var exception = new WalletItemAlreadyExistsException(
            message ?? $"The item {walletItemName} already exist in the category '{categoryId}'."
        );

        exception.Data["CategoryId"] = categoryId;
        exception.Data["WalletItemName"] = walletItemName;

        throw exception;
    }

    public static void Raise(Guid categoryId, string walletItemName)
    {
        Raise(categoryId, walletItemName, null);
    }
}

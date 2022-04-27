using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.WalletAggregateRoot.Exceptions;

[Serializable]
public class CategoryAlreadyExistsException : DomainExceptionBase
{
    private CategoryAlreadyExistsException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.CategoryAlreadyExists;

    public static void Raise(Guid walletId, string categoryName, string message)
    {
        var exception = new CategoryAlreadyExistsException(
            message ?? $"A category with name '{categoryName}' already exists in the wallet '{walletId}'."
        );
        exception.Data["WalletId"] = walletId;
        throw exception;
    }

    public static void Raise(Guid walletId, string categoryName)
    {
        Raise(walletId, categoryName, null);
    }
}

using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.POC.Domain.WalletAggregateRoot.Exceptions;

[Serializable]
public class CategoryDoesNotExistException : DomainExceptionBase
{
    private CategoryDoesNotExistException(
        string message
    ) : base(message)
    {
    }

    public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.CategoryDoesNotExist;

    public static void Raise(Guid walletId, Guid categoryId, string message)
    {
        var exception = new CategoryDoesNotExistException(
            message ?? $"A category with id '{categoryId}' does not exist in the wallet '{walletId}'."
        );

        exception.Data["WalletId"] = walletId;
        throw exception;
    }

    public static void Raise(Guid walletId, Guid categoryId)
    {
        Raise(walletId, categoryId, null);
    }
}

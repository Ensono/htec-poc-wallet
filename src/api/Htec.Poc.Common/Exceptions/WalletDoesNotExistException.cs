using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using Htec.Poc.Common.Operations;

namespace Htec.Poc.Common.Exceptions;

[Serializable]
public class WalletDoesNotExistException : ApplicationExceptionBase
{
    private WalletDoesNotExistException(
        ExceptionCode exceptionCode,
        OperationCode operationCode,
        Guid correlationId,
        string message
    ) : base((int)operationCode, correlationId, message)
    {
        ExceptionCode = (int)exceptionCode;

        HttpStatusCode = (int)ExceptionCodeToHttpStatusCodeConverter.GetHttpStatusCode((int)exceptionCode);
    }

    public override int ExceptionCode { get; protected set; }

    public static void Raise(OperationCode operationCode, Guid correlationId, Guid walletId, string message)
    {
        var exception = new WalletDoesNotExistException(
            Exceptions.ExceptionCode.WalletDoesNotExist,
            operationCode,
            correlationId,
            message ?? $"A wallet with id '{walletId}' does not exist."
        );
        exception.Data["WalletId"] = walletId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid walletId)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, walletId, null);
    }
}

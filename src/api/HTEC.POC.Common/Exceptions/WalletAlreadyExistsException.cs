using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using HTEC.POC.Common.Operations;

namespace HTEC.POC.Common.Exceptions;

[Serializable]
public class WalletAlreadyExistsException : ApplicationExceptionBase
{
    private WalletAlreadyExistsException(
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
        var exception = new WalletAlreadyExistsException(
            Exceptions.ExceptionCode.WalletAlreadyExists,
            operationCode,
            correlationId,
            message ?? $"A wallet with id '{walletId}' already exists."
        );
        exception.Data["WalletId"] = walletId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid walletId)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, walletId, null);
    }
}

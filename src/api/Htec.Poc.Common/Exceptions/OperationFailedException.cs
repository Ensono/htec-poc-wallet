using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using Htec.Poc.Common.Operations;

namespace Htec.Poc.Common.Exceptions;

[Serializable]
public class OperationFailedException : ApplicationExceptionBase
{
    public OperationFailedException(ExceptionCode exceptionCode, 
        OperationCode operationCode, 
        Guid correlationId, 
        string message) 
        : base((int) operationCode, 
            correlationId, 
            message)
    {
        ExceptionCode = (int)exceptionCode;

        HttpStatusCode = (int)ExceptionCodeToHttpStatusCodeConverter.GetHttpStatusCode((int)exceptionCode);
    }
       
    public override int ExceptionCode { get; protected set; }
       
    public static void Raise(IOperationContext context, Guid walletId, string message)
    {
        var exception = new OperationFailedException(
            Exceptions.ExceptionCode.OperationFailed, 
            (OperationCode) context.OperationCode,
            context.CorrelationId,
            message
        );
        exception.Data["WalletId"] = walletId;
        throw exception;
    }
}

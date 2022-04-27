using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using HTEC.POC.Common.Operations;

namespace HTEC.POC.Common.Exceptions;

[Serializable]
public class DomainRuleViolationException : ApplicationExceptionBase
{
    private DomainRuleViolationException(
        ExceptionCode exceptionCode,
        OperationCode operationCode,
        Guid correlationId,
        string message,
        Exception domainException
    ) : base((int)operationCode, correlationId, message, domainException)
    {
        ExceptionCode = (int)exceptionCode;

        HttpStatusCode = (int)ExceptionCodeToHttpStatusCodeConverter.GetHttpStatusCode((int)exceptionCode);
    }

    public override int ExceptionCode { get; protected set; }

    public static void Raise(OperationCode operationCode, Guid correlationId, Guid walletId, DomainExceptionBase domainException)
    {
        var exception = new DomainRuleViolationException(
            (ExceptionCode)domainException.ExceptionCode,
            operationCode,
            correlationId,
            $"A domain exception has been raised in the wallet '{walletId}'. {domainException.Message}",
            domainException
        );
        exception.Data["WalletId"] = walletId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid walletId, DomainExceptionBase domainException)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, walletId, domainException);
    }
}

using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using Htec.Poc.Common.Operations;

namespace Htec.Poc.Common.Exceptions;

[Serializable]
public class MemberDoesNotExistException : ApplicationExceptionBase
{
    private MemberDoesNotExistException(
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

    public static void Raise(OperationCode operationCode, Guid correlationId, Guid memberId, string message)
    {
        var exception = new MemberDoesNotExistException(
            Exceptions.ExceptionCode.MemberDoesNotExistException,
            operationCode,
            correlationId,
            message ?? $"A wallet with member id '{memberId}' does not exist."
        );
        exception.Data["MemberId"] = memberId;
        throw exception;
    }

    public static void Raise(IOperationContext context, Guid memberId)
    {
        Raise((OperationCode)context.OperationCode, context.CorrelationId, memberId, null);
    }
}
using System;
using Amido.Stacks.Core.Exceptions;
using Amido.Stacks.Core.Operations;
using HTEC.Engagement.Common.Operations;

namespace HTEC.Engagement.Common.Exceptions
{
    [Serializable]
    public class PointsAlreadyExistsException : ApplicationExceptionBase
    {
        private PointsAlreadyExistsException(
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

        public static void Raise(OperationCode operationCode, Guid correlationId, Guid pointsId, string message)
        {
            var exception = new PointsAlreadyExistsException(
                Exceptions.ExceptionCode.PointsAlreadyExists,
                operationCode,
                correlationId,
                message ?? $"A points with id '{pointsId}' already exists."
            );
            exception.Data["PointsId"] = pointsId;
            throw exception;
        }

        public static void Raise(IOperationContext context, Guid pointsId)
        {
            Raise((OperationCode)context.OperationCode, context.CorrelationId, pointsId, null);
        }
    }
}

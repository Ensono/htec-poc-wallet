using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.Engagement.Domain.PointsAggregateRoot.Exceptions
{
    [Serializable]
    public class RedemptionGreaterThanBalanceException : DomainExceptionBase
    {
        private RedemptionGreaterThanBalanceException(string message) : base(message)
        {
        }

        public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.PointsRedemptionGreaterThanBalanceException;

        public static void Raise(int points, string message)
        {
            var exception = new RedemptionGreaterThanBalanceException(
                message ?? $"The points redemption amount of {points} is greater than the points account balance."
            );

            exception.Data["ItemName"] = points;
            throw exception;
        }

        public static void Raise(int points)
        {
            Raise(points, null);
        }
    }
}

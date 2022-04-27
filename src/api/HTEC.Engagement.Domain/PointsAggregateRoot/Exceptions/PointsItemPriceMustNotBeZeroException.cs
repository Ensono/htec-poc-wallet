using System;
using Amido.Stacks.Core.Exceptions;

namespace HTEC.Engagement.Domain.PointsAggregateRoot.Exceptions
{
    [Serializable]
    public class PointsItemPriceMustNotBeZeroException : DomainExceptionBase
    {
        private PointsItemPriceMustNotBeZeroException(
            string message
        ) : base(message)
        {
        }

        public override int ExceptionCode { get; protected set; } = (int)Common.Exceptions.ExceptionCode.PointsItemPriceMustNotBeZero;

        public static void Raise(string itemName, string message)
        {
            var exception = new PointsItemPriceMustNotBeZeroException(
                message ?? $"The price for the item {itemName} had price as zero. A price must be provided."
            );

            exception.Data["ItemName"] = itemName;
            throw exception;
        }

        public static void Raise(string itemName)
        {
            Raise(itemName, null);
        }
    }
}

namespace HTEC.Engagement.Application.CQRS.Enums
{
    public enum EventCode
    {
        // Points operations
        PointsCreated = 101,
        PointsDeleted = 102,
        PointsIssued = 103,
        PointsRedeemed = 104,

        // CosmosDB change feed operations
        EntityUpdated = 999
    }
}

namespace HTEC.Engagement.Common.Events
{
    public enum EventCode
    {
        // Points operations
        PointsCreated = 101,
        PointsDeleted = 102,
        PointsIssued = 103,
        PointsRedeemed = 104,

        CosmosDbChangeFeedEvent = 999
    }
}

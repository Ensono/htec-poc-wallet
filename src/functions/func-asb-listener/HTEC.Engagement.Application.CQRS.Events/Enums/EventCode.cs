namespace HTEC.Engagement.Application.CQRS.Events.Enums
{
	public enum EventCode
	{
		// Points operations
		PointsCreated = 101,
		PointsUpdated = 102,
		PointsDeleted = 103,

		// Categories Operations
		CategoryCreated = 201,
		CategoryUpdated = 202,
		CategoryDeleted = 203,

		// Items Operations
		PointsItemCreated = 301,
		PointsItemUpdated = 302,
		PointsItemDeleted = 303,

		// CosmosDB change feed operations
		EntityUpdated = 999
	}
}

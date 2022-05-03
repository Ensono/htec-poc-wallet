namespace Htec.Poc.Application.CQRS.Events.Enums
{
	public enum EventCode
	{
        // Reward operations
        RewardCreated = 101,
        RewardUpdated = 102,
        RewardDeleted = 103,
        RewardCalculated = 104,

        // Categories Operations
        CategoryCreated = 201,
        CategoryUpdated = 202,
        CategoryDeleted = 203,

        // Items Operations
        RewardItemCreated = 301,
        RewardItemUpdated = 302,
        RewardItemDeleted = 303,

        // CosmosDB change feed operations
        EntityUpdated = 999
    }
}

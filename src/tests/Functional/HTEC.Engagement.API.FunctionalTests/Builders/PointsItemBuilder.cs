using HTEC.Engagement.API.FunctionalTests.Models;

namespace HTEC.Engagement.API.FunctionalTests.Builders
{
    public class PointsItemBuilder : IBuilder<PointsItemRequest>
    {
        private readonly PointsItemRequest pointsItem;

        public PointsItemBuilder()
        {
            pointsItem = new PointsItemRequest();
        }

        public PointsItemBuilder WithName(string name)
        {
            pointsItem.name = name;
            return this;
        }

        public PointsItemBuilder WithDescription(string description)
        {
            pointsItem.description = description;
            return this;
        }

        public PointsItemBuilder WithPrice(double price)
        {
            pointsItem.price = price;
            return this;
        }

        public PointsItemBuilder WithAvailablity(bool available)
        {
            pointsItem.available = available;
            return this;
        }

        public PointsItemRequest Build()
        {
            return pointsItem;
        }
        
        public PointsItemBuilder SetDefaultValues(string name)
        {
            pointsItem.name = name;
            pointsItem.description = "Item description";
            pointsItem.price = 12.50;
            pointsItem.available = true;
            return this;
        }
    }
}

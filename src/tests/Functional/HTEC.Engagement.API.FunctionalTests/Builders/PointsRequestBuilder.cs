using HTEC.Engagement.API.FunctionalTests.Models;

namespace HTEC.Engagement.API.FunctionalTests.Builders
{
    public class PointsRequestBuilder : IBuilder<PointsRequest>
    {
        private PointsRequest points;

        public PointsRequestBuilder()
        {
            points = new PointsRequest();
        }

        public PointsRequestBuilder SetDefaultValues(string name)
        {
            points.name = name;
            points.description = "Updated points description";
            points.enabled = true;
            return this;
        }

        public PointsRequestBuilder WithName(string name)
        {
            points.name = name;
            return this;
        }

        public PointsRequestBuilder WithDescription(string description)
        {
            points.description = description;
            return this;
        }

        public PointsRequestBuilder SetEnabled(bool enabled)
        {
            points.enabled = enabled;
            return this;
        }

        public PointsRequest Build()
        {
            return points;
        }
    }
}

using System;
using System.Collections.Generic;
using HTEC.Engagement.API.FunctionalTests.Models;

namespace HTEC.Engagement.API.FunctionalTests.Builders
{
    public class PointsBuilder : IBuilder<Points>
    {
        private Points points;

        public PointsBuilder()
        {
            points = new Points();
        }


        public PointsBuilder SetDefaultValues(string name)
        {
            var categoryBuilder = new CategoryBuilder();

            points.id = Guid.NewGuid().ToString();
            points.name = name;
            points.description = "Default test points description";
            points.enabled = true;
            points.categories = new List<Category>()
            {
                categoryBuilder.SetDefaultValues("Burgers")
                .Build()
            };

            return this;
        }

        public PointsBuilder WithId(Guid id)
        {
            points.id = id.ToString();
            return this;
        }

        public PointsBuilder WithName(string name)
        {
            points.name = name;
            return this;
        }

        public PointsBuilder WithDescription(string description)
        {
            points.description = description;
            return this;
        }

        public PointsBuilder WithNoCategories()
        {
            points.categories = new List<Category>();
            return this;
        }

        public PointsBuilder WithCategories(List<Category> categories)
        {
            points.categories = categories;
            return this;
        }

        public PointsBuilder SetEnabled(bool enabled)
        {
            points.enabled = enabled;
            return this;
        }

        public Points Build()
        {
            return points;
        }
    }
}

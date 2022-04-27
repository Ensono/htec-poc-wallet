using AutoFixture.Xunit2;
using Xunit;

namespace HTEC.Engagement.Domain.UnitTests
{
    [Trait("TestType", "UnitTests")]
    public class PointsTests
    {
        [Theory, AutoData]

        public void Update(Points points, string name, string description, bool enabled, int balance)
        {
            points.Update(name, description, enabled, balance);

            Assert.Equal(name, points.Name);
            Assert.Equal(description, points.Description);
            Assert.Equal(enabled, points.Enabled);
            Assert.Equal(balance, points.Balance);

            //TODO: When DDD story is complete, check if the events are raised
        }
    }
}

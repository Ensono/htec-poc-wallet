using TestStack.BDDfy;
using Xunit;
using HTEC.Engagement.API.FunctionalTests.Tests.Fixtures;
using HTEC.Engagement.API.FunctionalTests.Tests.Steps;

namespace HTEC.Engagement.API.FunctionalTests.Tests.Functional
{
    //Define the story/feature being tested
    [Story(AsA = "Administrator of a restaurant",
        IWant = "To be able to delete old pointss",
        SoThat = "Customers do not see out of date options")]
    public class DeletePointsTests : IClassFixture<AuthFixture>
    {
        private readonly PointsSteps steps;
        private readonly AuthFixture fixture;

        public DeletePointsTests(AuthFixture fixture)
        {
            //Get instances of the fixture and steps required for the test
            this.fixture = fixture;
            steps = new PointsSteps();
        }

        //Add all tests that make up the story to this class
        [Fact]
        public void Admins_Can_Delete_Pointss()
        {
            this.Given(step => fixture.GivenAUser())
                .And(step => steps.GivenAPointsAlreadyExists())
                .When(step => steps.WhenIDeleteAPoints())
                .Then(step => steps.ThenThePointsHasBeenDeleted())
                //Then(step => steps.ThenSomeActionIsMade())
                //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB).
                .BDDfy();
        }
    }
}

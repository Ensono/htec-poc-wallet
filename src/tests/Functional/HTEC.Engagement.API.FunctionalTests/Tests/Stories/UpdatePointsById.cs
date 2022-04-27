using TestStack.BDDfy;
using Xunit;
using HTEC.Engagement.API.FunctionalTests.Tests.Fixtures;
using HTEC.Engagement.API.FunctionalTests.Tests.Steps;

namespace HTEC.Engagement.API.FunctionalTests.Tests.Functional
{
    //Define the story/feature being tested
    [Story(AsA = "Administrator for a restaurant",
        IWant = "To be able to update existing pointss",
        SoThat = "The pointss are always showing our latest offerings"
        )]
    public class UpdatePointsById : IClassFixture<AuthFixture>
    {
        private readonly AuthFixture fixture;
        private readonly PointsSteps steps;

        public UpdatePointsById(AuthFixture fixture)
        {
            //Get instances of the fixture and steps required for the test
            this.fixture = fixture;
            steps = new PointsSteps();
        }

        //Add all tests that make up the story to this class
        [Fact]
        public void Admins_Can_Update_Existing_Pointss()
        {
            this.Given(s => fixture.GivenAnAdmin())
                .And(s => steps.GivenAPointsAlreadyExists())
                .When(s => steps.WhenISendAnUpdatePointsRequest())
                .Then(s => steps.ThenThePointsIsUpdatedCorrectly())
                //Then(step => categorySteps.ThenSomeActionIsMade())
                //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
                .BDDfy();
        }
    }
}

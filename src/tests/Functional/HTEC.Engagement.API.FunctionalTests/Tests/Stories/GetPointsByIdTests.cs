using TestStack.BDDfy;
using Xunit;
using HTEC.Engagement.API.FunctionalTests.Tests.Fixtures;
using HTEC.Engagement.API.FunctionalTests.Tests.Steps;

namespace HTEC.Engagement.API.FunctionalTests.Tests.Functional
{
    //Define the story/feature being tested
    [Story(
        AsA = "user of the Yumido website",
        IWant = "to be able to view specific pointss",
        SoThat = "I can choose what to eat")]
    public class GetPointsByIdTests : IClassFixture<AuthFixture>
    {
        private readonly AuthFixture fixture;
        private readonly PointsSteps steps;

        public GetPointsByIdTests(AuthFixture fixture)
        {
            //Get instances of the fixture and steps required for the test
            this.fixture = fixture;
            steps = new PointsSteps();
        }

        //Add all tests that make up the story to this class.
        [Fact]
        public void Users_Can_View_Existing_Pointss()
        {
            this.Given(s => fixture.GivenAUser())
                .And(s => steps.GivenAPointsAlreadyExists())
                .When(s => steps.WhenIGetAPoints())
                .Then(s => steps.ThenICanReadThePointsReturned())
                .BDDfy();
        }

        [Fact]
        [Trait("Category", "SmokeTest")]
        public void Admins_Can_View_Existing_Pointss()
        {
            this.Given(s => fixture.GivenAnAdmin())
                .And(s => steps.GivenAPointsAlreadyExists())
                .When(s => steps.WhenIGetAPoints())
                .Then(s => steps.ThenICanReadThePointsReturned())
                .BDDfy();
        }
    }
}

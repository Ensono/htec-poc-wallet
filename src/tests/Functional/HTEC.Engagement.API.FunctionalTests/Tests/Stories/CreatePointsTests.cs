using TestStack.BDDfy;
using Xunit;
using HTEC.Engagement.API.FunctionalTests.Tests.Fixtures;
using HTEC.Engagement.API.FunctionalTests.Tests.Steps;

namespace HTEC.Engagement.API.FunctionalTests.Tests.Functional
{
    //Define the story/feature being tested
    [Story(
        AsA = "restaurant administrator",
        IWant = "to be able to create pointss",
        SoThat = "customers know what we have on offer")]

    public class CreatePointsTests : IClassFixture<AuthFixture>
    {
        private readonly PointsSteps steps;
        private readonly AuthFixture fixture;

        public CreatePointsTests(AuthFixture fixture)
        {
            //Get instances of the fixture and steps required for the test
            this.fixture = fixture;
            steps = new PointsSteps();
        }

        //Add all tests that make up the story to this class.
        [Fact]
        public void Create_a_points()
        {
            this.Given(step => fixture.GivenAUser())
                .Given(step => steps.GivenIHaveSpecfiedAFullPoints())
                .When(step => steps.WhenICreateThePoints())
                .Then(step => steps.ThenThePointsHasBeenCreated())
                //.Then(step => steps.ThenSomeActionIsMade())
                //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
                .BDDfy();
        }
    }
}

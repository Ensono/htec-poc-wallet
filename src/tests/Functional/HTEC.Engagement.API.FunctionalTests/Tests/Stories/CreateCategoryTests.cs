using TestStack.BDDfy;
using Xunit;
using HTEC.Engagement.API.FunctionalTests.Tests.Fixtures;
using HTEC.Engagement.API.FunctionalTests.Tests.Steps;

namespace HTEC.Engagement.API.FunctionalTests.Tests.Stories
{
    //Define the story/feature being tested
    [Story(
        AsA = "restaurant administrator",
        IWant = "to be able to create pointss with categories",
        SoThat = "customers know what we have on offer")]
    public class CreateCategoryTests : IClassFixture<AuthFixture>
    {
        private readonly CategorySteps categorySteps;
        private readonly AuthFixture fixture;


        public CreateCategoryTests(AuthFixture fixture)
        {
            //Get instances of the fixture and steps required for the test
            this.fixture = fixture;
            categorySteps = new CategorySteps();
        }
        
        //Add all tests that make up the story to this class.
        [Fact]
        public void Create_category_for_points()
        {
            this.Given(step => fixture.GivenAUser())
                .And(step => categorySteps.GivenIHaveSpecfiedAFullCategory())
                .When(step => categorySteps.WhenICreateTheCategoryForAnExistingPoints())
                .Then(step => categorySteps.ThenTheCategoryHasBeenCreated())
                // .Then(step => categorySteps.ThenSomeActionIsMade())
                //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
                .BDDfy();
        }
    }
}
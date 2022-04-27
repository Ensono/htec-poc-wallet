using TestStack.BDDfy;
using Xunit;
using HTEC.POC.API.FunctionalTests.Tests.Fixtures;
using HTEC.POC.API.FunctionalTests.Tests.Steps;

namespace HTEC.POC.API.FunctionalTests.Tests.Functional;

//Define the story/feature being tested
[Story(
    AsA = "restaurant administrator",
    IWant = "to be able to create wallets",
    SoThat = "customers know what we have on offer")]

public class CreateWalletTests : IClassFixture<AuthFixture>
{
    private readonly WalletSteps steps;
    private readonly AuthFixture fixture;

    public CreateWalletTests(AuthFixture fixture)
    {
        //Get instances of the fixture and steps required for the test
        this.fixture = fixture;
        steps = new WalletSteps();
    }

    //Add all tests that make up the story to this class.
    [Fact]
    public void Create_a_wallet()
    {
        this.Given(step => fixture.GivenAUser())
            .Given(step => steps.GivenIHaveSpecfiedAFullWallet())
            .When(step => steps.WhenICreateTheWallet())
            .Then(step => steps.ThenTheWalletHasBeenCreated())
            //.Then(step => steps.ThenSomeActionIsMade())
            //This step is to verify the outcome of the event in the Subcriber. (e.g. a field is updated in DB, blob is created and so on).
            .BDDfy();
    }
}
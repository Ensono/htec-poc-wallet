using Htec.Poc.Application.CQRS.Events;
using Xbehave;
using Xunit;
using Htec.Poc.API.ComponentTests.Fixtures;

namespace Htec.Poc.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class CreateWalletFeature
{
    /* SCENARIOS: Create a wallet

         Examples:
         -----------------------------------------------------------------
        | AsRole              | Wallet Condition     | Outcome              |
        |---------------------|--------------------|----------------------|
        | Admin               | Valid Wallet         | 201 Resource Create  |
        | Admin               | Invalid Wallet       | 400 Bad  Request     |
        | Employee, Customer,                                             |
        | UnauthenticatedUser | Valid Wallet         | 403 Forbidden        |

    */

    [Scenario, CustomAutoData]
    public void CreateWalletAsAdminForValidWalletShouldSucceed(CreateWalletFixture fixture)
    {
        "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
        "And a valid wallet being submitted".x(fixture.GivenAValidWallet);
        "And the wallet does not does not exist".x(fixture.GivenAWalletDoesNotExist);
        "When the wallet is submitted".x(fixture.WhenTheWalletCreationIsSubmitted);
        "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        "And the response code is CREATED".x(fixture.ThenACreatedResponseIsReturned);
        "And the id of the new wallet is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
        "And the wallet data is submitted correctly to the database".x(fixture.ThenTheWalletIsSubmittedToDatabase);
        $"And an event of type {nameof(WalletCreatedEvent)} is raised".x(fixture.ThenAWalletCreatedEventIsRaised);
    }

    [Scenario, CustomAutoData]
    public void CreateWalletAsAdminForInvalidWalletShouldFail(CreateWalletFixture fixture)
    {
        "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
        "And a valid wallet being submitted".x(fixture.GivenAInvalidWallet);
        "And the wallet does not does not exist".x(fixture.GivenAWalletDoesNotExist);
        "When the wallet is submitted".x(fixture.WhenTheWalletCreationIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the wallet is not submitted to the database".x(fixture.ThenTheWalletIsNotSubmittedToDatabase);
        $"And an event of type {nameof(WalletCreatedEvent)} should not be raised".x(fixture.ThenAWalletCreatedEventIsNotRaised);
    }

    [Scenario(Skip = "Only works when Auth is implemented")]
    [CustomInlineAutoData("Employee")]
    [CustomInlineAutoData("Customer")]
    [CustomInlineAutoData("UnauthenticatedUser")]
    public void CreateWalletAsNonAdminForValidWalletShouldFail(string role, CreateWalletFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And a valid wallet being submitted".x(fixture.GivenAValidWallet);
        "And the wallet does not does not exist".x(fixture.GivenAWalletDoesNotExist);
        "When the wallet is submitted".x(fixture.WhenTheWalletCreationIsSubmitted);
        "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
        "And the wallet is not submitted to the database".x(fixture.ThenTheWalletIsNotSubmittedToDatabase);
        $"And an event of type {nameof(WalletCreatedEvent)} should not be raised".x(fixture.ThenAWalletCreatedEventIsNotRaised);
    }
}

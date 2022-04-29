using Xbehave;
using Htec.Poc.API.ComponentTests.Fixtures;

namespace Htec.Poc.API.ComponentTests.Features;

public class CreateCategoryFeature
{
    /* SCENARIOS: Create a category in the wallet
      
         Examples: 
         ---------------------------------------------------------------------------------
        | AsRole              | Existing Wallet | Existing Category  | Outcome              |
        |---------------------|---------------|--------------------|----------------------|
        | Admin               | Yes           | No                 | 200 OK               |
        | Employee            | Yes           | No                 | 200 OK               |
        | Admin               | No            | No                 | 404 Not Found        |
        | Admin               | Yes           | Yes                | 409 Conflict         |
        | Customer            | Yes           | No                 | 403 Forbidden        |
        | UnauthenticatedUser | Yes           | No                 | 403 Forbidden        |

    */

    [Scenario]
    [CustomInlineAutoData("Admin")]
    [CustomInlineAutoData("Employee")]
    public void CreateCategoryShouldSucceed(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing wallet".x(fixture.GivenAnExistingWallet);
        "And the wallet belongs to the user restaurant".x(fixture.GivenTheWalletBelongsToUserRestaurant);
        "And the category being created does not exist in the wallet".x(fixture.GivenTheCategoryDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        "And the wallet is loaded from the storage".x(fixture.ThenWalletIsLoadedFromStorage);
        "And the id of the new category is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
        "And the category is added to the wallet".x(fixture.ThenTheCategoryIsAddedToWallet);
        "And the wallet is persisted to the storage".x(fixture.ThenTheWalletShouldBePersisted);
        "And the event WalletUpdate is Raised".x(fixture.ThenAWalletUpdatedEventIsRaised);
        "And the event CategoryCreated is Raised".x(fixture.ThenACategoryCreatedEventIsRaised);
    }

    [Scenario]
    [CustomInlineAutoData("Admin")]
    public void CreateCategoryShouldFailWhenWalletDoesNotExist(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And a wallet does not exist".x(fixture.GivenAWalletDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
        "And the wallet is loaded from the storage".x(fixture.ThenWalletIsLoadedFromStorage);
        "And the wallet is not persisted to the storage".x(fixture.ThenTheWalletShouldNotBePersisted);
        "And the event WalletUpdate should NOT be raised".x(fixture.ThenAWalletUpdatedEventIsNotRaised);
        "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

    [Scenario]
    [CustomInlineAutoData("Admin")]
    public void CreateCategoryShouldFailWhenCategoryAlreadyExists(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing wallet".x(fixture.GivenAnExistingWallet);
        "And the wallet belongs to the user restaurant".x(fixture.GivenTheWalletBelongsToUserRestaurant);
        "And the category being created already exist in the wallet".x(fixture.GivenTheCategoryAlreadyExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is Conflict".x(fixture.ThenAConflictResponseIsReturned);
        "And the wallet is NOT persisted to the storage".x(fixture.ThenTheWalletShouldNotBePersisted);
        "And the event WalletUpdate should NOT be raised".x(fixture.ThenAWalletUpdatedEventIsNotRaised);
        "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

    [Scenario(Skip = "Disabled until security is implemented")]
    [CustomInlineAutoData("Customer")]
    [CustomInlineAutoData("UnauthenticatedUser")]
    public void CreateCategoryShouldFailWithForbidden(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing wallet".x(fixture.GivenAnExistingWallet);
        "And the category being created does not exist in the wallet".x(fixture.GivenTheCategoryDoesNotExist);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
        "And the wallet is not persisted to the storage".x(fixture.ThenTheWalletShouldNotBePersisted);
        "And the event WalletUpdate is NOT Raised".x(fixture.ThenAWalletUpdatedEventIsNotRaised);
        "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }


    [Scenario(Skip = "Disabled until security is implemented")]
    [CustomInlineAutoData("Admin")]
    [CustomInlineAutoData("Employee")]
    public void CreateCategoryShouldFailWhenWalletDoesNotBelongToUser(string role, CreateCategoryFixture fixture)
    {
        $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
        "And an existing wallet".x(fixture.GivenAnExistingWallet);
        "And the wallet does not belong to users restaurant".x(fixture.GivenTheWalletDoesNotBelongToUserRestaurant);
        "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
        "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
        "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
        "And the wallet is loaded from the storage".x(fixture.ThenWalletIsLoadedFromStorage);
        "And the wallet is not persisted to the storage".x(fixture.ThenTheWalletShouldNotBePersisted);
        "And the event WalletUpdate should not be Raised".x(fixture.ThenAWalletUpdatedEventIsNotRaised);
        "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
    }

}

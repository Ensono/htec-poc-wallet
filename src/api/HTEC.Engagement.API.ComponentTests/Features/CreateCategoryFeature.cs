//using Xbehave;
//using HTEC.Engagement.API.ComponentTests.Fixtures;

//namespace HTEC.Engagement.API.ComponentTests.Features
//{
//    public class CreateCategoryFeature
//    {
//        /* SCENARIOS: Create a category in the points
          
//             Examples: 
//             ---------------------------------------------------------------------------------
//            | AsRole              | Existing Points | Existing Category  | Outcome              |
//            |---------------------|---------------|--------------------|----------------------|
//            | Admin               | Yes           | No                 | 200 OK               |
//            | Employee            | Yes           | No                 | 200 OK               |
//            | Admin               | No            | No                 | 404 Not Found        |
//            | Admin               | Yes           | Yes                | 409 Conflict         |
//            | Customer            | Yes           | No                 | 403 Forbidden        |
//            | UnauthenticatedUser | Yes           | No                 | 403 Forbidden        |

//        */

//        [Scenario]
//        [CustomInlineAutoData("Admin")]
//        [CustomInlineAutoData("Employee")]
//        public void CreateCategoryShouldSucceed(string role, CreateCategoryFixture fixture)
//        {
//            $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
//            "And an existing points".x(fixture.GivenAnExistingPoints);
//            "And the points belongs to the user restaurant".x(fixture.GivenThePointsBelongsToUserRestaurant);
//            "And the category being created does not exist in the points".x(fixture.GivenTheCategoryDoesNotExist);
//            "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
//            "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
//            "And the points is loaded from the storage".x(fixture.ThenPointsIsLoadedFromStorage);
//            "And the id of the new category is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
//            "And the category is added to the points".x(fixture.ThenTheCategoryIsAddedToPoints);
//            "And the points is persisted to the storage".x(fixture.ThenThePointsShouldBePersisted);
//            "And the event PointsUpdate is Raised".x(fixture.ThenAPointsUpdatedEventIsRaised);
//            "And the event CategoryCreated is Raised".x(fixture.ThenACategoryCreatedEventIsRaised);
//        }

//        [Scenario]
//        [CustomInlineAutoData("Admin")]
//        public void CreateCategoryShouldFailWhenPointsDoesNotExist(string role, CreateCategoryFixture fixture)
//        {
//            $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
//            "And a points does not exist".x(fixture.GivenAPointsDoesNotExist);
//            "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
//            "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
//            "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
//            "And the points is loaded from the storage".x(fixture.ThenPointsIsLoadedFromStorage);
//            "And the points is not persisted to the storage".x(fixture.ThenThePointsShouldNotBePersisted);
//            "And the event PointsUpdate should NOT be raised".x(fixture.ThenAPointsUpdatedEventIsNotRaised);
//            "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
//        }

//        [Scenario]
//        [CustomInlineAutoData("Admin")]
//        public void CreateCategoryShouldFailWhenCategoryAlreadyExists(string role, CreateCategoryFixture fixture)
//        {
//            $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
//            "And an existing points".x(fixture.GivenAnExistingPoints);
//            "And the points belongs to the user restaurant".x(fixture.GivenThePointsBelongsToUserRestaurant);
//            "And the category being created already exist in the points".x(fixture.GivenTheCategoryAlreadyExist);
//            "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
//            "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
//            "And the response code is Conflict".x(fixture.ThenAConflictResponseIsReturned);
//            "And the points is NOT persisted to the storage".x(fixture.ThenThePointsShouldNotBePersisted);
//            "And the event PointsUpdate should NOT be raised".x(fixture.ThenAPointsUpdatedEventIsNotRaised);
//            "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
//        }

//        [Scenario(Skip = "Disabled until security is implemented")]
//        [CustomInlineAutoData("Customer")]
//        [CustomInlineAutoData("UnauthenticatedUser")]
//        public void CreateCategoryShouldFailWithForbidden(string role, CreateCategoryFixture fixture)
//        {
//            $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
//            "And an existing points".x(fixture.GivenAnExistingPoints);
//            "And the category being created does not exist in the points".x(fixture.GivenTheCategoryDoesNotExist);
//            "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
//            "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
//            "And the points is not persisted to the storage".x(fixture.ThenThePointsShouldNotBePersisted);
//            "And the event PointsUpdate is NOT Raised".x(fixture.ThenAPointsUpdatedEventIsNotRaised);
//            "And the event CategoryCreated is NOT Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
//        }


//        [Scenario(Skip = "Disabled until security is implemented")]
//        [CustomInlineAutoData("Admin")]
//        [CustomInlineAutoData("Employee")]
//        public void CreateCategoryShouldFailWhenPointsDoesNotBelongToUser(string role, CreateCategoryFixture fixture)
//        {
//            $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
//            "And an existing points".x(fixture.GivenAnExistingPoints);
//            "And the points does not belong to users restaurant".x(fixture.GivenThePointsDoesNotBelongToUserRestaurant);
//            "When a new category is submitted".x(fixture.WhenTheCategoryIsSubmitted);
//            "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
//            "And the response code is NotFound".x(fixture.ThenANotFoundResponseIsReturned);
//            "And the points is loaded from the storage".x(fixture.ThenPointsIsLoadedFromStorage);
//            "And the points is not persisted to the storage".x(fixture.ThenThePointsShouldNotBePersisted);
//            "And the event PointsUpdate should not be Raised".x(fixture.ThenAPointsUpdatedEventIsNotRaised);
//            "And the event CategoryCreated should not be Raised".x(fixture.ThenACategoryCreatedEventIsNotRaised);
//        }

//    }
//}

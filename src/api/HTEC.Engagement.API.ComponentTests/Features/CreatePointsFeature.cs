using HTEC.Engagement.Application.CQRS.Events;
using Xbehave;
using Xunit;
using HTEC.Engagement.API.ComponentTests.Fixtures;

namespace HTEC.Engagement.API.ComponentTests.Features
{
    [Trait("TestType", "ComponentTests")]
    public class CreatePointsFeature
    {
        /* SCENARIOS: Create a points

             Examples:
             -----------------------------------------------------------------
            | AsRole              | Points Condition     | Outcome              |
            |---------------------|--------------------|----------------------|
            | Admin               | Valid Points         | 201 Resource Create  |
            | Admin               | Invalid Points       | 400 Bad  Request     |
            | Employee, Customer,                                             |
            | UnauthenticatedUser | Valid Points         | 403 Forbidden        |

        */

        [Scenario, CustomAutoData]
        public void CreatePointsAsAdminForValidPointsShouldSucceed(CreatePointsFixture fixture)
        {
            "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
            "And a valid points being submitted".x(fixture.GivenAValidPoints);
            "And the points does not does not exist".x(fixture.GivenAPointsDoesNotExist);
            "When the points is submitted".x(fixture.WhenThePointsCreationIsSubmitted);
            "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
            "And the response code is CREATED".x(fixture.ThenACreatedResponseIsReturned);
            "And the id of the new points is returned".x(fixture.ThenTheResourceCreatedResponseIsReturned);
            "And the points data is submitted correctly to the database".x(fixture.ThenThePointsIsSubmittedToDatabase);
            $"And an event of type {nameof(PointsCreatedEvent)} is raised".x(fixture.ThenAPointsCreatedEventIsRaised);
        }

        [Scenario, CustomAutoData]
        public void CreatePointsAsAdminForInvalidPointsShouldFail(CreatePointsFixture fixture)
        {
            "Given the user is authenticated and has the Admin role".x(() => fixture.GivenTheUserIsAnAuthenticatedAdministrator());
            "And a valid points being submitted".x(fixture.GivenAInvalidPoints);
            "And the points does not does not exist".x(fixture.GivenAPointsDoesNotExist);
            "When the points is submitted".x(fixture.WhenThePointsCreationIsSubmitted);
            "Then a failure response is returned".x(fixture.ThenAFailureResponseIsReturned);
            "And the points is not submitted to the database".x(fixture.ThenThePointsIsNotSubmittedToDatabase);
            $"And an event of type {nameof(PointsCreatedEvent)} should not be raised".x(fixture.ThenAPointsCreatedEventIsNotRaised);
        }

        [Scenario(Skip = "Only works when Auth is implemented")]
        [CustomInlineAutoData("Employee")]
        [CustomInlineAutoData("Customer")]
        [CustomInlineAutoData("UnauthenticatedUser")]
        public void CreatePointsAsNonAdminForValidPointsShouldFail(string role, CreatePointsFixture fixture)
        {
            $"Given the user is authenticated and has the {role} role".x(() => fixture.GivenTheUserIsAuthenticatedAndHasRole(role));
            "And a valid points being submitted".x(fixture.GivenAValidPoints);
            "And the points does not does not exist".x(fixture.GivenAPointsDoesNotExist);
            "When the points is submitted".x(fixture.WhenThePointsCreationIsSubmitted);
            "Then a Forbidden response is returned".x(fixture.ThenAForbiddenResponseIsReturned);
            "And the points is not submitted to the database".x(fixture.ThenThePointsIsNotSubmittedToDatabase);
            $"And an event of type {nameof(PointsCreatedEvent)} should not be raised".x(fixture.ThenAPointsCreatedEventIsNotRaised);
        }
    }
}

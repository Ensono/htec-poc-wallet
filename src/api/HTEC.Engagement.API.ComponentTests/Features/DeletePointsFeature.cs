using Xunit;

namespace HTEC.Engagement.API.ComponentTests.Features
{
    [Trait("TestType", "ComponentTests")]
    public class DeletePointsFeature
    {
        /* SCENARIOS: Delete a points
          
             Examples: 
             -------------------------------------------------------------------------
            | AsRole                        | Points Condition   | Outcome              |
            |-------------------------------|------------------|----------------------|
            | Admin, Employee               | Valid Points       | 204 No Content       |
            | Admin                         | Invalid Points     | 400 Bad  Request     |
            | Admin                         | Points not exist   | 404 Bad  Request     |
            | Customer, UnauthenticatedUser | Valid Points       | 403 Forbidden        |

        */

        //TODO: Implement test scenarios
    }
}

using Xunit;

namespace HTEC.Engagement.API.ComponentTests.Features
{
    [Trait("TestType", "ComponentTests")]
    public class UpdatePointsFeature
    {
        /* SCENARIOS: Update a points
          
             Examples: 
             -----------------------------------------------------------------------------------
            | AsRole                        | Points Condition             | Outcome              |
            |-------------------------------|----------------------------|----------------------|
            | Admin, Employee               | Valid Points                 | 204 No Content       |
            | Admin, Employee               | Points from other restaurant | 404 Not found        |
            | Admin, Employee               | Invalid Points               | 400 Bad  Request     | 
            | Customer, UnauthenticatedUser | Valid Points                 | 403 Forbidden        |

        */

        //TODO: Implement test scenarios
    }
}

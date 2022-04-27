using Xunit;

namespace HTEC.POC.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class DeleteWalletFeature
{
    /* SCENARIOS: Delete a wallet
      
         Examples: 
         -------------------------------------------------------------------------
        | AsRole                        | Wallet Condition   | Outcome              |
        |-------------------------------|------------------|----------------------|
        | Admin, Employee               | Valid Wallet       | 204 No Content       |
        | Admin                         | Invalid Wallet     | 400 Bad  Request     |
        | Admin                         | Wallet not exist   | 404 Bad  Request     |
        | Customer, UnauthenticatedUser | Valid Wallet       | 403 Forbidden        |

    */

    //TODO: Implement test scenarios
}

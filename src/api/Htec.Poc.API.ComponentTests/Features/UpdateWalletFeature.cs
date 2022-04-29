using Xunit;

namespace Htec.Poc.API.ComponentTests.Features;

[Trait("TestType", "ComponentTests")]
public class UpdateWalletFeature
{
    /* SCENARIOS: Update a wallet
      
         Examples: 
         -----------------------------------------------------------------------------------
        | AsRole                        | Wallet Condition             | Outcome              |
        |-------------------------------|----------------------------|----------------------|
        | Admin, Employee               | Valid Wallet                 | 204 No Content       |
        | Admin, Employee               | Wallet from other restaurant | 404 Not found        |
        | Admin, Employee               | Invalid Wallet               | 400 Bad  Request     | 
        | Customer, UnauthenticatedUser | Valid Wallet                 | 403 Forbidden        |

    */

    //TODO: Implement test scenarios
}

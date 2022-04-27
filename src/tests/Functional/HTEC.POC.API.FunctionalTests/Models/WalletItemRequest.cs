namespace HTEC.POC.API.FunctionalTests.Models;

public class WalletItemRequest
{
    public string name { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    public bool available { get; set; }
}
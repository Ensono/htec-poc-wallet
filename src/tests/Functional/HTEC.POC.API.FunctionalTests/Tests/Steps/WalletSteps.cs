using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HTEC.POC.API.FunctionalTests.Builders;
using HTEC.POC.API.FunctionalTests.Configuration;
using HTEC.POC.API.FunctionalTests.Models;
using HTEC.POC.Tests.Api.Builders.Http;

namespace HTEC.POC.API.FunctionalTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Wallet endpoints
/// </summary>
public class WalletSteps
{
    private WalletRequest createWalletRequest;
    private WalletRequest updateWalletRequest;
    private HttpResponseMessage lastResponse;
    public string existingWalletId;
    private readonly string baseUrl;
    public const string walletPath = "v1/wallet/";

    public WalletSteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        baseUrl = config.BaseUrl;
    }

    #region Step Definitions

    #region Given

    public async Task<string> GivenAWalletAlreadyExists()
    {
        createWalletRequest = new WalletRequestBuilder()
            .SetDefaultValues("Yumido Test Wallet")
            .Build();

        try
        {
            lastResponse = await HttpRequestFactory.Post(baseUrl, walletPath, createWalletRequest);

            if (lastResponse.StatusCode == HttpStatusCode.Created)
            {
                existingWalletId = JsonConvert
                    .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            throw new Exception(
                $"Wallet could not be created. API response: {await lastResponse.Content.ReadAsStringAsync()}");
        }

        return existingWalletId;
    }

    public void GivenIHaveSpecfiedAFullWallet()
    {
        createWalletRequest = new WalletRequestBuilder()
            .SetDefaultValues("Yumido Test Wallet")
            .Build();
    }

    #endregion Given

    #region When

    public async Task WhenISendAnUpdateWalletRequest()
    {
        updateWalletRequest = new WalletRequestBuilder()
            .WithName("Updated Wallet Name")
            .WithDescription("Updated Description")
            .SetEnabled(true)
            .Build();

        lastResponse = await HttpRequestFactory.Put(baseUrl, $"{walletPath}{existingWalletId}", updateWalletRequest);
    }

    public async Task WhenICreateTheWallet()
    {
        lastResponse = await HttpRequestFactory.Post(baseUrl, walletPath, createWalletRequest);
    }

    public async Task WhenIDeleteAWallet()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl, $"{walletPath}{existingWalletId}");
    }

    public async Task WhenIGetAWallet()
    {
        lastResponse = await HttpRequestFactory.Get(baseUrl, $"{walletPath}{existingWalletId}");
    }

    #endregion When

    #region Then

    public void ThenTheWalletHasBeenCreated()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public void ThenTheWalletHasBeenDeleted()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public async Task ThenICanReadTheWalletReturned()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.OK,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var responseWallet = JsonConvert.DeserializeObject<Wallet>(await lastResponse.Content.ReadAsStringAsync());

        //compare the initial request sent to the API against the actual response
        responseWallet.name.ShouldBe(createWalletRequest.name,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");

        responseWallet.description.ShouldBe(createWalletRequest.description,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");

        responseWallet.enabled.ShouldBe(createWalletRequest.enabled,
            $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");
    }

    public async Task ThenTheWalletIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{walletPath}{existingWalletId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateWalletResponse =
                JsonConvert.DeserializeObject<Wallet>(await updatedResponse.Content.ReadAsStringAsync());

            updateWalletResponse.name.ShouldBe(updateWalletRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");

            updateWalletResponse.description.ShouldBe(updateWalletRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");

            updateWalletResponse.enabled.ShouldBe(updateWalletRequest.enabled,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");
        }
        else
        {
            //throw exception rather than use assertions if the GET request fails as GET is not the subject of the test
            //Assertions should only be used on the subject of the test
            throw new Exception($"Could not retrieve the updated wallet using GET /wallet/{existingWalletId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}
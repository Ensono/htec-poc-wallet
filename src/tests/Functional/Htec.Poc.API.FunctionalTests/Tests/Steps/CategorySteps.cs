using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Htec.Poc.API.FunctionalTests.Builders;
using Htec.Poc.API.FunctionalTests.Configuration;
using Htec.Poc.API.FunctionalTests.Models;
using Htec.Poc.Tests.Api.Builders.Http;

namespace Htec.Poc.API.FunctionalTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Category endpoints
/// </summary>
public class CategorySteps
{
    private readonly string baseUrl;
    private HttpResponseMessage lastResponse;
    private string existingWalletId;
    private CategoryRequest createCategoryRequest;
    private CategoryRequest updateCategoryRequest;
    private string existingCategoryId;
    private const string categoryPath = "/category/";
    private readonly WalletSteps walletSteps = new WalletSteps();

    public CategorySteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        baseUrl = config.BaseUrl;
    }

    #region Step Definitions

    #region Given

    public void GivenIHaveSpecfiedAFullCategory()
    {
        createCategoryRequest = new CategoryRequestBuilder()
            .SetDefaultValues("Yumido Test Category")
            .Build();
    }

    #endregion Given

    #region When

    public async Task<string> WhenICreateTheCategoryForAnExistingWallet()
    {
        existingWalletId = await walletSteps.GivenAWalletAlreadyExists();

        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{WalletSteps.walletPath}{existingWalletId}{categoryPath}", createCategoryRequest);

        existingCategoryId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
        return existingCategoryId;
    }

    public async Task<string> CreateCategoryForSpecificWallet(String walletId)
    {
        lastResponse = await HttpRequestFactory.Post(baseUrl,
            $"{WalletSteps.walletPath}{walletId}{categoryPath}",
            new CategoryRequestBuilder()
                .SetDefaultValues("Yumido Test Category")
                .Build());

        existingCategoryId = JsonConvert
            .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
        return existingCategoryId;
    }

    public async Task WhenISendAnUpdateCategoryRequest()
    {
        updateCategoryRequest = new CategoryRequestBuilder()
            .WithName("Updated Category Name")
            .WithDescription("Updated Category Description")
            .Build();
        String path = $"{WalletSteps.walletPath}{walletSteps.existingWalletId}{categoryPath}{existingCategoryId}";

        lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateCategoryRequest);
    }

    public async Task WhenIDeleteTheCategory()
    {
        lastResponse = await HttpRequestFactory.Delete(baseUrl,
            $"{WalletSteps.walletPath}{existingWalletId}{categoryPath}{existingCategoryId}");
    }

    #endregion When

    #region Then

    public void ThenTheCategoryHasBeenCreated()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
    }

    public async Task ThenTheCategoryHasBeenDeleted()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var getCurrentWallet = await HttpRequestFactory.Get(baseUrl, $"{WalletSteps.walletPath}{existingWalletId}");
        if (getCurrentWallet.StatusCode == HttpStatusCode.OK)
        {
            var getCurrentWalletResponse =
                JsonConvert.DeserializeObject<Wallet>(await getCurrentWallet.Content.ReadAsStringAsync());

            getCurrentWalletResponse.categories.ShouldBeEmpty();
        }
    }

    public async Task ThenTheCategoryIsUpdatedCorrectly()
    {
        lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
            $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

        var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{WalletSteps.walletPath}{existingWalletId}");

        if (updatedResponse.StatusCode == HttpStatusCode.OK)
        {
            var updateCategoryResponse =
                JsonConvert.DeserializeObject<Wallet>(await updatedResponse.Content.ReadAsStringAsync());


            updateCategoryResponse.categories[0].name.ShouldBe(updateCategoryRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");

            updateCategoryResponse.categories[0].description.ShouldBe(updateCategoryRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the wallet as expected");
        }
        else
        {
            throw new Exception($"Could not retrieve the updated wallet using GET /wallet/{existingWalletId}");
        }
    }

    #endregion Then

    #endregion Step Definitions
}
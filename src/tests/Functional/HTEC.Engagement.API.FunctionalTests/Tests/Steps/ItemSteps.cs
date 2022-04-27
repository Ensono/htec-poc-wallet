using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HTEC.Engagement.API.FunctionalTests.Builders;
using HTEC.Engagement.API.FunctionalTests.Configuration;
using HTEC.Engagement.API.FunctionalTests.Models;
using HTEC.Engagement.Tests.Api.Builders.Http;

namespace HTEC.Engagement.API.FunctionalTests.Tests.Steps
{
    /// <summary>
    /// These are the steps required for testing the Category endpoints
    /// </summary>
    public class ItemSteps
    {
        private readonly PointsSteps pointsSteps = new PointsSteps();
        private readonly CategorySteps categorySteps = new CategorySteps();
        private readonly string baseUrl;
        private HttpResponseMessage lastResponse;
        private string existingPointsId;
        private string existingCategoryId;
        private string existingItemId;
        private PointsItemRequest createItemRequest;
        private PointsItemRequest updateItemRequest;
        private const string categoryPath = "/category/";
        private const string itemPath = "/items/";

        public ItemSteps()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            baseUrl = config.BaseUrl;
        }

        #region Step Definitions

        #region Given

        public void GivenIHaveSpecfiedAFullItem()
        {
            createItemRequest = new PointsItemBuilder()
                .SetDefaultValues("Yumido Test Item")
                .Build();
        }

        #endregion Given

        #region When

        public async Task WhenISendAnUpdateItemRequest()
        {
            updateItemRequest = new PointsItemBuilder()
                .WithName("Updated item name")
                .WithDescription("Updated item description")
                .WithPrice(4.5)
                .WithAvailablity(true)
                .Build();
            String path =
                $"{PointsSteps.pointsPath}{existingPointsId}{categoryPath}{existingCategoryId}{itemPath}{existingItemId}";

            lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateItemRequest);
        }

        public async Task WhenICreateTheItemForAnExistingPointsAndCategory()
        {
            existingPointsId = await pointsSteps.GivenAPointsAlreadyExists();
            existingCategoryId = await categorySteps.CreateCategoryForSpecificPoints(existingPointsId);

            lastResponse = await HttpRequestFactory.Post(baseUrl,
                $"{PointsSteps.pointsPath}{existingPointsId}{categoryPath}{existingCategoryId}{itemPath}", createItemRequest);
            existingItemId = JsonConvert
                .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
        }

        public async Task WhenIDeleteTheItem()
        {
            lastResponse = await HttpRequestFactory.Delete(baseUrl,
                $"{PointsSteps.pointsPath}{existingPointsId}{categoryPath}{existingCategoryId}{itemPath}{existingItemId}");
        }

        #endregion When

        #region Then

        public void ThenTheItemHasBeenCreated()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
        }

        public async Task ThenTheItemHasBeenDeleted()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

            var getCurrentPoints = await HttpRequestFactory.Get(baseUrl, $"{PointsSteps.pointsPath}{existingPointsId}");
            if (getCurrentPoints.StatusCode == HttpStatusCode.OK)
            {
                var getCurrentPointsResponse =
                    JsonConvert.DeserializeObject<Points>(await getCurrentPoints.Content.ReadAsStringAsync());

                getCurrentPointsResponse.categories[0].items.ShouldBeEmpty();
            }
        }

        public async Task ThenTheItemIsUpdatedCorrectly()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

            var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{PointsSteps.pointsPath}{existingPointsId}");

            if (updatedResponse.StatusCode == HttpStatusCode.OK)
            {
                var updateItemResponse =
                    JsonConvert.DeserializeObject<Points>(await updatedResponse.Content.ReadAsStringAsync());


                updateItemResponse.categories[0].items[0].name.ShouldBe(updateItemRequest.name,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

                updateItemResponse.categories[0].items[0].description.ShouldBe(updateItemRequest.description,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

                updateItemResponse.categories[0].items[0].price.ShouldBe(updateItemRequest.price.ToString(),
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");
                updateItemResponse.categories[0].items[0].available.ShouldBeTrue();
            }
            else
            {
                throw new Exception($"Could not retrieve the updated points using GET /points/{existingPointsId}");
            }
        }

        #endregion Then

        #endregion Step Definitions
    }
}
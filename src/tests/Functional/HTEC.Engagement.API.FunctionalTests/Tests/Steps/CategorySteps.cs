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
    public class CategorySteps
    {
        private readonly string baseUrl;
        private HttpResponseMessage lastResponse;
        private string existingPointsId;
        private CategoryRequest createCategoryRequest;
        private CategoryRequest updateCategoryRequest;
        private string existingCategoryId;
        private const string categoryPath = "/category/";
        private readonly PointsSteps pointsSteps = new PointsSteps();

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

        public async Task<string> WhenICreateTheCategoryForAnExistingPoints()
        {
            existingPointsId = await pointsSteps.GivenAPointsAlreadyExists();

            lastResponse = await HttpRequestFactory.Post(baseUrl,
                $"{PointsSteps.pointsPath}{existingPointsId}{categoryPath}", createCategoryRequest);

            existingCategoryId = JsonConvert
                .DeserializeObject<CreateObjectResponse>(await lastResponse.Content.ReadAsStringAsync()).id;
            return existingCategoryId;
        }

        public async Task<string> CreateCategoryForSpecificPoints(String pointsId)
        {
            lastResponse = await HttpRequestFactory.Post(baseUrl,
                $"{PointsSteps.pointsPath}{pointsId}{categoryPath}",
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
            String path = $"{PointsSteps.pointsPath}{pointsSteps.existingPointsId}{categoryPath}{existingCategoryId}";

            lastResponse = await HttpRequestFactory.Put(baseUrl, path, updateCategoryRequest);
        }

        public async Task WhenIDeleteTheCategory()
        {
            lastResponse = await HttpRequestFactory.Delete(baseUrl,
                $"{PointsSteps.pointsPath}{existingPointsId}{categoryPath}{existingCategoryId}");
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

            var getCurrentPoints = await HttpRequestFactory.Get(baseUrl, $"{PointsSteps.pointsPath}{existingPointsId}");
            if (getCurrentPoints.StatusCode == HttpStatusCode.OK)
            {
                var getCurrentPointsResponse =
                    JsonConvert.DeserializeObject<Points>(await getCurrentPoints.Content.ReadAsStringAsync());

                getCurrentPointsResponse.categories.ShouldBeEmpty();
            }
        }

        public async Task ThenTheCategoryIsUpdatedCorrectly()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

            var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{PointsSteps.pointsPath}{existingPointsId}");

            if (updatedResponse.StatusCode == HttpStatusCode.OK)
            {
                var updateCategoryResponse =
                    JsonConvert.DeserializeObject<Points>(await updatedResponse.Content.ReadAsStringAsync());


                updateCategoryResponse.categories[0].name.ShouldBe(updateCategoryRequest.name,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

                updateCategoryResponse.categories[0].description.ShouldBe(updateCategoryRequest.description,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");
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
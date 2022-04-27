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
    /// These are the steps required for testing the Points endpoints
    /// </summary>
    public class PointsSteps
    {
        private PointsRequest createPointsRequest;
        private PointsRequest updatePointsRequest;
        private HttpResponseMessage lastResponse;
        public string existingPointsId;
        private readonly string baseUrl;
        public const string pointsPath = "v1/points/";

        public PointsSteps()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            baseUrl = config.BaseUrl;
        }

        #region Step Definitions

        #region Given

        public async Task<string> GivenAPointsAlreadyExists()
        {
            createPointsRequest = new PointsRequestBuilder()
                .SetDefaultValues("Yumido Test Points")
                .Build();

            try
            {
                lastResponse = await HttpRequestFactory.Post(baseUrl, pointsPath, createPointsRequest);

                if (lastResponse.StatusCode == HttpStatusCode.Created)
                {
                    existingPointsId = JsonConvert
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
                    $"Points could not be created. API response: {await lastResponse.Content.ReadAsStringAsync()}");
            }

            return existingPointsId;
        }

        public void GivenIHaveSpecfiedAFullPoints()
        {
            createPointsRequest = new PointsRequestBuilder()
                .SetDefaultValues("Yumido Test Points")
                .Build();
        }

        #endregion Given

        #region When

        public async Task WhenISendAnUpdatePointsRequest()
        {
            updatePointsRequest = new PointsRequestBuilder()
                .WithName("Updated Points Name")
                .WithDescription("Updated Description")
                .SetEnabled(true)
                .Build();

            lastResponse = await HttpRequestFactory.Put(baseUrl, $"{pointsPath}{existingPointsId}", updatePointsRequest);
        }

        public async Task WhenICreateThePoints()
        {
            lastResponse = await HttpRequestFactory.Post(baseUrl, pointsPath, createPointsRequest);
        }

        public async Task WhenIDeleteAPoints()
        {
            lastResponse = await HttpRequestFactory.Delete(baseUrl, $"{pointsPath}{existingPointsId}");
        }

        public async Task WhenIGetAPoints()
        {
            lastResponse = await HttpRequestFactory.Get(baseUrl, $"{pointsPath}{existingPointsId}");
        }

        #endregion When

        #region Then

        public void ThenThePointsHasBeenCreated()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
        }

        public void ThenThePointsHasBeenDeleted()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");
        }

        public async Task ThenICanReadThePointsReturned()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.OK,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

            var responsePoints = JsonConvert.DeserializeObject<Points>(await lastResponse.Content.ReadAsStringAsync());

            //compare the initial request sent to the API against the actual response
            responsePoints.name.ShouldBe(createPointsRequest.name,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

            responsePoints.description.ShouldBe(createPointsRequest.description,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

            responsePoints.enabled.ShouldBe(createPointsRequest.enabled,
                $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");
        }

        public async Task ThenThePointsIsUpdatedCorrectly()
        {
            lastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
                $"Response from {lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} was not as expected");

            var updatedResponse = await HttpRequestFactory.Get(baseUrl, $"{pointsPath}{existingPointsId}");

            if (updatedResponse.StatusCode == HttpStatusCode.OK)
            {
                var updatePointsResponse =
                    JsonConvert.DeserializeObject<Points>(await updatedResponse.Content.ReadAsStringAsync());

                updatePointsResponse.name.ShouldBe(updatePointsRequest.name,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

                updatePointsResponse.description.ShouldBe(updatePointsRequest.description,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");

                updatePointsResponse.enabled.ShouldBe(updatePointsRequest.enabled,
                    $"{lastResponse.RequestMessage.Method} {lastResponse.RequestMessage.RequestUri} did not create the points as expected");
            }
            else
            {
                //throw exception rather than use assertions if the GET request fails as GET is not the subject of the test
                //Assertions should only be used on the subject of the test
                throw new Exception($"Could not retrieve the updated points using GET /points/{existingPointsId}");
            }
        }

        #endregion Then

        #endregion Step Definitions
    }
}
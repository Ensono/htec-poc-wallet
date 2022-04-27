using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.API.ContractTests.Fixtures
{
    public class ProviderStateMiddleware : IMiddleware
    {
        private readonly IDictionary<string, Action> providerStates;
        readonly IPointsRepository repository;

        public ProviderStateMiddleware(IPointsRepository repository)
        {
            this.repository = repository;

            //Provider states are from the Given clause in the consumer tests
            providerStates = new Dictionary<string, Action>
            {
                {
                    //These are case sensitive. Consumer and Provider should share list of states when states are required
                    "An existing points", 
                    ExistingPoints
                },
                {
                    "A points does not exist",
                    PointsDoesNotExist
                }
            };
        }

        //These functions set up the mocked provider API state by mocking the response the repository gives
        private void ExistingPoints()
        {
            var points = new Fixture().Create<Points>();

            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(points));
            repository.SaveAsync(Arg.Any<Points>()).Returns(true);
        }

        private void PointsDoesNotExist()
        {
            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult((Points)null));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                HandleProviderStatesRequest(context);
                await context.Response.WriteAsync(string.Empty);
            }
            else
            {
                await next(context);
            }
        }

        private void HandleProviderStatesRequest(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                context.Request.Body != null)
            {
                var jsonRequestBody = string.Empty;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = reader.ReadToEnd();
                }

                var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                //A null or empty provider state key must be handled
                if (providerState != null && !string.IsNullOrEmpty(providerState.State))
                {
                    providerStates[providerState.State].Invoke();
                }
            }
        }

    }
}

using System.Threading;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HTEC.Engagement.Domain;
//using HTEC.Engagement.Domain.Entities;

namespace HTEC.Engagement.Infrastructure.HealthChecks
{
    /// <summary>
    /// Implement health for a resource that does not implement IHealthCheck
    /// Each resource should have it's own implementation
    /// Resources implementing IHealthCheck should be added directly to the pipeline:
    /// i.e: services.AddHealthChecks().AddCheck<CosmosDbDocumentStorage<Points>>("CosmosDB");
    /// </summary>
    public class CustomHealthCheck : IHealthCheck
    {
        //Example: do not add this example, CosmosDBStorage already implements IHealthCheck
        readonly IDocumentStorage<Points> documentStorage;

        public CustomHealthCheck(IDocumentStorage<Points> documentStorage)
        {
            this.documentStorage = documentStorage;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            //
            //Example: do not add this example, CosmosDBStorage already implements IHealthCheck
            //var id = Guid.NewGuid().ToString();
            //var document = await this.documentStorage.GetByIdAsync(id, id);

            return await Task.FromResult(HealthCheckResult.Healthy($"{nameof(CustomHealthCheck)}: OK"));
        }
    }
}

using System;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents.Abstractions;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Infrastructure.Repositories
{
    public class CosmosDbPointsRepository : IPointsRepository
    {
        readonly IDocumentStorage<Points> documentStorage;

        public CosmosDbPointsRepository(IDocumentStorage<Points> documentStorage)
        {
            this.documentStorage = documentStorage;
        }

        public async Task<Points> GetByIdAsync(Guid id)
        {
            var result = await documentStorage.GetByIdAsync(id.ToString(), id.ToString());

            //TODO: Publish request charge results

            return result.Content;
        }

        public async Task<bool> SaveAsync(Points entity)
        {
            //TODO: Handle etag
            //TODO: Publish request charge results

            var result = await documentStorage.SaveAsync(entity.Id.ToString(), entity.Id.ToString(), entity, null);

            return result.IsSuccessful;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            //TODO: Publish request charge results

            var result = await documentStorage.DeleteAsync(id.ToString(), id.ToString());

            return result.IsSuccessful;
        }
    }
}


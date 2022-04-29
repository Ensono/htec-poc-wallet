using System;
using System.Threading.Tasks;
using Amido.Stacks.Data.Documents.Abstractions;
using Htec.Poc.Application.Integration;
using Htec.Poc.Domain;

namespace Htec.Poc.Infrastructure.Repositories;

public class CosmosDbWalletRepository : IWalletRepository
{
    readonly IDocumentStorage<Wallet> documentStorage;

    public CosmosDbWalletRepository(IDocumentStorage<Wallet> documentStorage)
    {
        this.documentStorage = documentStorage;
    }

    public async Task<Wallet> GetByIdAsync(Guid id)
    {
        var result = await documentStorage.GetByIdAsync(id.ToString(), id.ToString());

        //TODO: Publish request charge results

        return result.Content;
    }

    public async Task<bool> SaveAsync(Wallet entity)
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

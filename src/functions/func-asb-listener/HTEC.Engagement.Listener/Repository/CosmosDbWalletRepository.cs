using Amido.Stacks.Data.Documents.Abstractions;
using System;
using System.Threading.Tasks;

namespace HTEC.Engagement.Listener.Repository
{
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

            return result.Content;
        }
    }
}
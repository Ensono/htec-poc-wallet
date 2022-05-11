using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Amido.Stacks.Data.Documents.Abstractions;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Queries.GetWalletById;

namespace Htec.Poc.Application.QueryHandlers;

public class GetWalletByMemberIdQueryHandler : IQueryHandler<GetWalletByMemberId, Wallet>
{
    private readonly IWalletRepository repository;
    readonly IDocumentSearch<Domain.Wallet> storage;

    public GetWalletByMemberIdQueryHandler(IWalletRepository repository, IDocumentSearch<Domain.Wallet> storage)
    {
        this.repository = repository;
        this.storage = storage;
    }

    public async Task<Wallet> ExecuteAsync(GetWalletByMemberId criteria)
    {
        if (criteria == null)
            throw new ArgumentException("A valid GetWalletByMemberId os required!");

        var results = await storage.Search(filter => filter.MemberId == criteria.MemberId);

        if (!results.IsSuccessful)
            return null;

        var wallet = results.Content.FirstOrDefault();

        var result = Wallet.FromDomain(wallet);

        return result;
    }
}
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using Htec.Poc.Application.Integration;
using Htec.Poc.CQRS.Queries.GetWalletById;

namespace Htec.Poc.Application.QueryHandlers;

public class GetWalletByMemberIdQueryHandler : IQueryHandler<GetWalletByMemberId, Wallet>
{
    private readonly IWalletRepository repository;

    public GetWalletByMemberIdQueryHandler(IWalletRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Wallet> ExecuteAsync(GetWalletByMemberId criteria)
    {
        var wallet = await repository.GetByIdAsync(criteria.MemberId);

        if (wallet == null)
            return null;

        //You might prefer using AutoMapper in here
        var result = Wallet.FromDomain(wallet);

        return result;
    }
}
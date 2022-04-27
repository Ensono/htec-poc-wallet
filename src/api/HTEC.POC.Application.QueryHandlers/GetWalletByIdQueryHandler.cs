using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using HTEC.POC.Application.Integration;
using HTEC.POC.CQRS.Queries.GetWalletById;

namespace HTEC.POC.Application.QueryHandlers;

public class GetWalletByIdQueryHandler : IQueryHandler<GetWalletById, Wallet>
{
    private readonly IWalletRepository repository;

    public GetWalletByIdQueryHandler(IWalletRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Wallet> ExecuteAsync(GetWalletById criteria)
    {
        var wallet = await repository.GetByIdAsync(criteria.Id);

        if (wallet == null)
            return null;

        //You might prefer using AutoMapper in here
        var result = Wallet.FromDomain(wallet);

        return result;
    }
}

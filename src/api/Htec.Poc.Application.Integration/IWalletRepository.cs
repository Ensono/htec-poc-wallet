using System;
using System.Threading.Tasks;
using Htec.Poc.Domain;

namespace Htec.Poc.Application.Integration;

public interface IWalletRepository
{
    Task<Wallet> GetByIdAsync(Guid id);
    Task<bool> SaveAsync(Wallet entity);
    Task<bool> DeleteAsync(Guid id);
}

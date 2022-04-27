using System;
using System.Threading.Tasks;
using HTEC.POC.Domain;

namespace HTEC.POC.Application.Integration;

public interface IWalletRepository
{
    Task<Wallet> GetByIdAsync(Guid id);
    Task<bool> SaveAsync(Wallet entity);
    Task<bool> DeleteAsync(Guid id);
}

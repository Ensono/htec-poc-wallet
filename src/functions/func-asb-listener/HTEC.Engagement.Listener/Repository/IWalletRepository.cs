using System;
using System.Threading.Tasks;

namespace HTEC.Engagement.Listener.Repository
{
    public interface IWalletRepository
    {
        Task<Wallet> GetByIdAsync(Guid id);
    }
}
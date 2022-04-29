using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htec.Poc.Application.Integration;
using Htec.Poc.Domain;

namespace Htec.Poc.Infrastructure.Fakes;

public class InMemoryWalletRepository : IWalletRepository
{
    private static readonly Object @lock = new Object();
    private static readonly Dictionary<Guid, Wallet> storage = new Dictionary<Guid, Wallet>();

    public async Task<Wallet> GetByIdAsync(Guid id)
    {
        if (storage.ContainsKey(id))
            return await Task.FromResult(storage[id]);
        else
            return await Task.FromResult((Wallet)null);
    }

    public async Task<bool> SaveAsync(Wallet entity)
    {
        if (entity == null)
            return await Task.FromResult(false);

        lock (@lock)
        {
            if (storage.ContainsKey(entity.Id))
                storage[entity.Id] = entity;
            else
                storage.Add(entity.Id, entity);
        }

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool result;
        lock (@lock)
        {
            if (!storage.ContainsKey(id))
                return false;

            result = storage.Remove(id);
        }

        return await Task.FromResult(result);
    }

}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTEC.Engagement.Application.Integration;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Infrastructure.Fakes
{
    public class InMemoryPointsRepository : IPointsRepository
    {
        private static readonly Object @lock = new Object();
        private static readonly Dictionary<Guid, Points> storage = new Dictionary<Guid, Points>();

        public async Task<Points> GetByIdAsync(Guid id)
        {
            if (storage.ContainsKey(id))
                return await Task.FromResult(storage[id]);
            else
                return await Task.FromResult((Points)null);
        }

        public async Task<bool> SaveAsync(Points entity)
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
}

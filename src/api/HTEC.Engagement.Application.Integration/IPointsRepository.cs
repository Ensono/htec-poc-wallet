using System;
using System.Threading.Tasks;
using HTEC.Engagement.Domain;

namespace HTEC.Engagement.Application.Integration
{
    public interface IPointsRepository
    {
        Task<Points> GetByIdAsync(Guid id);
        Task<bool> SaveAsync(Points entity);
        Task<bool> DeleteAsync(Guid id);
    }
}

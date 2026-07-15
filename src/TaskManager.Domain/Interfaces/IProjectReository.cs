using TaskManager.Domain.Entities.Aggregates;

namespace TaskManager.Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetByOwnerIdAsync(Guid ownerId);
        Task<IEnumerable<Project>> GetUserProjectsAsync(Guid userId);
    }
}
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Aggregates;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(TaskManagerDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<IEnumerable<Project>> GetByOwnerIdAsync(Guid ownerId)
    {
        return await _dbSet.Where(x => x.OwnerId == ownerId).ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetUserProjectsAsync(Guid userId)
    {
        return await _dbSet.Where(p => p.Members.Any(x => x.UserId == userId)).ToListAsync();
    }
}
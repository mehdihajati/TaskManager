using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class TaskRepository : Repository<TaskItem>, ITaskRepository
{
    public TaskRepository(TaskManagerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<TaskItem>> GetByAssigneeIdAsync(Guid assigneeId)
    {
        return await _dbSet.Where(x => x.AssigneeId == assigneeId).ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbSet.Where(x => x.ProjectId == projectId).ToListAsync();

    }
}
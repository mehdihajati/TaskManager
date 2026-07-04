using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskItem>> GetByAssigneeIdAsync(Guid assigneeId);
    }
}
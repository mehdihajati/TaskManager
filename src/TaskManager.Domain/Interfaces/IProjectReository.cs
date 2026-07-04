using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities.Aggregates;

namespace TaskManager.Domain.Interfaces
{
    public interface IProjectReository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetByOwnerIdAsync(Guid ownerId);
        Task<IEnumerable<Project>> GetUserProjectsAsync(Guid userId);
    }
}
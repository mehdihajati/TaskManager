using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities.Aggregates;

namespace TaskManager.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email); // for login
        Task<bool> ExistsAsync(string email);
    }
}
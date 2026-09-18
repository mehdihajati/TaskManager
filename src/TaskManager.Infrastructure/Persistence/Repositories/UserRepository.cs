using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities.Aggregates;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TaskManagerDbContext dbContext) : base(dbContext)
    {

    }
    public async Task<bool> ExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(x => x.Email == email);


    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}
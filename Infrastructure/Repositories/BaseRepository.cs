using Core.Interfaces.Data;
using Core.Models.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private ApplicationDbContext _dbContext;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        public virtual async Task<T> GetByIdAsync(long Id) => await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(c => c.ID == Id);

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var result = _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task DeleteByIdAsync(long Id)
        {
            var result = await GetByIdAsync(Id);
            if (result != null)
            {
                _dbContext.Set<T>().Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

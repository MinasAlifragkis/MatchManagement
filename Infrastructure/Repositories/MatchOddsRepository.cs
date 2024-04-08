using Core.Interfaces.Data;
using Core.Models.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MatchOddsRepository : BaseRepository<MatchOdds>, IMatchOddsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MatchOddsRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<MatchOdds>> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<MatchOdds>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Set<MatchOdds>().Include(c => c.Match).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}

using Core.Interfaces.Data;
using Core.Models.Entities;
using Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<Match>> logger) : base(dbContext, logger)
        {
        }
    }
}

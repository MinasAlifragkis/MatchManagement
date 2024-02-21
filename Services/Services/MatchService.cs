using AutoMapper;
using Core.Interfaces.Data;
using Core.Models.DTO;
using Core.Models.Entities;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Services;

namespace Services.Services
{
    public class MatchService : BaseService<Match, MatchDTO>, IMatchService
    {
        public MatchService(IMapper mapper, IMatchRepository repository, ILogger<MatchService> logger) : base(mapper, repository, logger)
        {}
    }
}

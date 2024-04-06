using AutoMapper;
using Core.Interfaces.Data;
using Core.Models.DTO;
using Core.Models.Entities;
using Core.Models.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MatchOddsService : BaseService<MatchOdds, MatchOddsDTO>, IMatchOddsService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ILogger _logger;

        public MatchOddsService(IMatchRepository matchRepository, IMapper mapper, IMatchOddsRepository repository, IDistributedCache redisCache, ILogger<MatchOddsService> logger) : base(mapper, repository, redisCache, logger)
        {
            _matchRepository = matchRepository;
            _logger = logger;
        }

        public override async Task<ResponseDTO<MatchOddsDTO>> AddAsync(MatchOddsDTO entityDTO)
        {
            var response = new ResponseDTO<MatchOddsDTO>();
            try
            {
                var match = await _matchRepository.GetByIdAsync(entityDTO.MatchId);
                if (match == null)
                {
                    response.Succeeded = false;
                    response.Error = new ErrorDTO
                    {
                        ErrorCode = ErrorCode.MatchNotFound
                    };
                    return response;
                }
                return await base.AddAsync(entityDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Succeeded = false;
                response.Error = new ErrorDTO
                {
                    ErrorCode = ErrorCode.UnknownError
                };
            }
            return response;
        }
    }
}

using Asp.Versioning;
using Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Services;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class MatchOddsController : BaseController<MatchOddsDTO>
    {
        public MatchOddsController(IMatchOddsService matchOddsService, ILogger<MatchOddsController> logger) : base(matchOddsService, logger)
        {}

        /// <summary>
        /// Creates a new MatchOdds
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>A newly created matchOdds</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /MatchOdds
        ///     {
        ///         "matchId": 1,
        ///         "specifier": "X",
        ///         "odd": 1.5
        ///     }
        /// </remarks>
        public override Task<IActionResult> AddAsync([FromBody] MatchOddsDTO entity)
        {
            return base.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing MatchOdds
        /// </summary>
        /// <param name="Id">Id of matchOdds</param>
        /// <param name="entity">MatchOdds</param>
        /// <returns>Updated matchOdds</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /MatchOdds/1
        ///     {
        ///         "id": 0,
        ///         "matchId": 1,
        ///         "specifier": "X",
        ///         "odd": 1.75
        ///     }
        ///
        /// </remarks>
        public override Task<IActionResult> UpdateAsync([FromRoute] long Id, [FromBody] MatchOddsDTO entity)
        {
            return base.UpdateAsync(Id, entity);
        }
    }
}

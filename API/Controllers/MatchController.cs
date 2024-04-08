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
    public class MatchController : BaseController<MatchDTO>
    {
        public MatchController(IMatchService matchService, ILogger<MatchController> logger) : base(matchService, logger)
        {}

        /// <summary>
        /// Creates a new Match
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>A newly created match</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Match
        ///     {
        ///         "description": "OSFP-PAO",
        ///         "matchDate": "20/02/2024",
        ///         "matchTime": "12:00",
        ///         "teamA": "OSFP",
        ///         "teamB": "PAO",
        ///         "sport": 1
        ///     }
        /// </remarks>
        public override Task<IActionResult> AddAsync([FromBody] MatchDTO entity)
        {
            return base.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing Match
        /// </summary>
        /// <param name="Id">Id of match</param>
        /// <param name="entity">Match</param>
        /// <returns>Updated match</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Match/1
        ///     {
        ///         "id": 1,
        ///         "description": "OSFP-AEK",
        ///         "matchDate": "20/02/2024",
        ///         "matchTime": "12:00",
        ///         "teamA": "OSFP",
        ///         "teamB": "AEK",
        ///         "sport": 1
        ///     }
        ///
        /// </remarks>
        public override Task<IActionResult> UpdateAsync([FromRoute] long Id, [FromBody] MatchDTO entity)
        {
            return base.UpdateAsync(Id, entity);
        }
    }
}

using Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Interfaces.Services;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase where T : BaseDTO
    {
        private readonly IBaseService<T> _baseService;
        private readonly ILogger<BaseController<T>> _logger;

        public BaseController(IBaseService<T> baseService, ILogger<BaseController<T>> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddAsync([FromBody]T entity)
        {
            _logger.LogDebug($"Add => received entity {JsonConvert.SerializeObject(entity)}");
            var result = await _baseService.AddAsync(entity);
            _logger.LogDebug($"Add => response {JsonConvert.SerializeObject(result)}");
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>All Items</returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            _logger.LogDebug("GetAll");
            var result = await _baseService.GetAllAsync(pageNumber, pageSize);
            _logger.LogDebug($"GetAll => received entity {JsonConvert.SerializeObject(result)}");
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The requested item</returns>
        [HttpGet("/api/[controller]/{Id}")]
        public virtual async Task<IActionResult> GetByIdAsync([FromRoute]long Id)
        {
            _logger.LogDebug($"GetById => Id {Id}");
            var result = await _baseService.GetByIdAsync(Id);
            _logger.LogDebug($"GetById => response {JsonConvert.SerializeObject(result)}");
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("/api/[controller]/{Id}")]
        public virtual async Task<IActionResult> UpdateAsync([FromRoute] long Id, [FromBody] T entity)
        {
            _logger.LogDebug($"Update => received Id/entity {Id}/{JsonConvert.SerializeObject(entity)}");
            var result = await _baseService.UpdateAsync(Id, entity);
            _logger.LogDebug($"Update => response {JsonConvert.SerializeObject(result)}");
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        /// <summary>
        /// Deletes an item by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("/api/[controller]/{Id}")]
        public virtual async Task<IActionResult> DeleteByIdAsync([FromRoute] long Id)
        {
            _logger.LogDebug($"DeleteById => Id {Id}");
            var result = await _baseService.DeleteByIdAsync(Id);
            _logger.LogDebug($"DeleteById => response {JsonConvert.SerializeObject(result)}");
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}

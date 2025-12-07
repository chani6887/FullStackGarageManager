using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using WorkshopManagement.Api.Models.Mongo;
using WorkshopManagement.Api.Services;

namespace WorkshopManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopRepository _repository;
        private readonly IMemoryCache _cache;
        private const string WorkshopsCacheKey = "AllWorkshopsData";
        private static readonly TimeSpan CacheExpiry = TimeSpan.FromMinutes(30);

        public WorkshopController(IWorkshopRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkshopData>), 200)]
        public async Task<IActionResult> GetAllWorkshops()
        {
            if (_cache.TryGetValue(WorkshopsCacheKey, out IEnumerable<WorkshopData> workshops))
            {
                return Ok(workshops);
            }

            workshops = await _repository.GetAllAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CacheExpiry);

            _cache.Set(WorkshopsCacheKey, workshops, cacheEntryOptions);

            return Ok(workshops);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddWorkshop([FromBody] WorkshopData newWorkshop)
        {
            if (newWorkshop == null || newWorkshop.WorkshopNumber <= 0)
            {
                return BadRequest("Workshop data is invalid.");
            }

            newWorkshop.Id = ObjectId.GenerateNewId().ToString();

            await _repository.AddAsync(newWorkshop);

            _cache.Remove(WorkshopsCacheKey);

            return Ok();
        }
    }
}

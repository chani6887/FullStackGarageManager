using Microsoft.AspNetCore.Mvc;
using WorkshopManagement.Api.Models;
using WorkshopManagement.Api.Services;

namespace WorkshopManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GaragesController : ControllerBase
    {
        private readonly GarageService _garageService;
        private readonly ExternalGarageService _externalService;

        public GaragesController(GarageService garageService, ExternalGarageService externalService)
        {
            _garageService = garageService;
            _externalService = externalService;
        }

        // 1) משיכת נתונים מה-API הממשלתי (לא שומר)
        [HttpGet("fromgov")]
        public async Task<IActionResult> GetFromGov([FromQuery] int limit = 5)
        {
            var items = await _externalService.FetchFromGovAsync(limit);
            return Ok(items);
        }

        // 2) קבלת פריטים שנשמרו בבסיס הנתונים
        [HttpGet]
        public async Task<IActionResult> GetSaved()
        {
            var list = await _garageService.GetAllAsync();
            return Ok(list);
        }

        // 3) הוספת מוסכים — נתבצע דה־דופינג בצד השרת
        [HttpPost("add")]
        public async Task<IActionResult> AddMany([FromBody] List<Garage> garages)
        {
            if (garages == null || !garages.Any()) return BadRequest("No garages provided");

            var toInsert = new List<Garage>();
            foreach (var g in garages)
            {
                bool exists = false;
                if (!string.IsNullOrEmpty(g.ExternalId))
                    exists = await _garageService.ExistsByExternalIdAsync(g.ExternalId);

                if (!exists)
                    exists = await _garageService.ExistsByNameAndAddressAsync(g.Name, g.Address);

                if (!exists)
                    toInsert.Add(g);
            }

            if (toInsert.Any())
                await _garageService.AddManyAsync(toInsert);

            var saved = await _garageService.GetAllAsync();
            return Ok(saved);
        }
    }
}

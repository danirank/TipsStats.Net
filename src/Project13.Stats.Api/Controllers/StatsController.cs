using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project13.Stats.Core.Data;
using Project13.Stats.Core.Models;
using Project13.Stats.Core.Services;
using static Project13.Stats.Core.Services.StatsService;

namespace Project13.Stats.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        private readonly StatsService _service;

        public StatsController(StatsService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<ActionResult<List<CalculationDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }



        [HttpGet("search")]
        public async Task<ActionResult<List<CalculationDto>>> Search(
        [FromQuery] StatsFilter filter)
        {
            var result = await _service.SearchAsync(filter);
            return Ok(result);
        }

        [HttpGet("analyze")]
        public async Task<ActionResult<SearchAnalysisResult>> Analyze([FromQuery] StatsFilter filter, [FromQuery] int maxSample = 1000)
        {
            var result = await _service.AnalyzeAsync(filter, maxSample);
            return Ok(result);
        }

    }
}

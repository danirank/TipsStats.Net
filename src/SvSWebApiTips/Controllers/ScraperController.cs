using Microsoft.AspNetCore.Mvc;
using SvSWebApiTips.Models;
using SvSWebApiTips.Services;
using SvSWebApiTips.Mappings;
using SvSWebApiTips.Constants;


namespace SvSWebApiTips.Controllers
{
    [ApiController]
    [Route("api/svenskaspel/")]
    [Produces("application/json")]

    public class ScraperController : ControllerBase
    {
        private readonly ScraperService _scraperService;
        public ScraperController(ScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchInfoDto>>> GetKupong([FromQuery] TipType tips)
        {
            try
            {
                var resultat = await _scraperService.GetKupong(tips);
                var dto = resultat.Select(m => m.ToDto());
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ett fel uppstod vid hämtning av kupongen: {ex.Message}");
            }
        }
    }
}

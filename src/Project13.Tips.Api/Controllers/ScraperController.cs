using Microsoft.AspNetCore.Mvc;
using Project13.Tips.Api.Models;
using Project13.Tips.Api.Services;
using Project13.Tips.Api.Mappings;
using Project13.Tips.Api.Constants;


namespace Project13.Tips.Api.Controllers
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
        public async Task<ActionResult<CouponDto>> GetKupong([FromQuery] TipType tipsTyp)
        {
            try
            {
                var resultat = await _scraperService.GetKupong(tipsTyp);
                return Ok(resultat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ett fel uppstod vid hämtning av kupongen: {ex.Message}");
            }
        }


    }
}

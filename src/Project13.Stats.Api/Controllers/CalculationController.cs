using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project13.Stats.Core.Data;
using Project13.Stats.Core.Models;
using Project13.Stats.Core.Services;

namespace Project13.Stats.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly CalculationService _calc;
        private readonly AppDbContext _db;

        public CalculationController(CalculationService calc, AppDbContext db)
        {
            _calc = calc;
            _db = db;
        }

        // GET: api/calculation
        [HttpGet]
        public async Task<ActionResult<List<CalculationModel>>> GetAllCalculations()
        {
            var summering = await _db.Summering.AsNoTracking().ToListAsync();
            var detaljer = await _db.Detaljer.AsNoTracking().ToListAsync();

            var result = _calc.Calculate(summering, detaljer);

            return Ok(result);
        }

        // GET: api/calculation/1234
        [HttpGet("{id}")]
        public async Task<ActionResult<Summering>> GetById(int id)
        {
            var firstSummering = await _db.Summering.FindAsync(id);

            if (firstSummering == null)
                return NotFound();

            return Ok(firstSummering);
        }
    }
}

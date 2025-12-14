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
        public async Task<List<CalculationModel>> GetAllCalculations()
        {
            var summering = await _db.Summering.AsNoTracking().ToListAsync();
            var detaljer = await _db.Detaljer.AsNoTracking().ToListAsync();

            var result = _calc.Calculate(summering, detaljer);

            return result;
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
        [HttpPost]
        public async Task<int> SeedCalculationsToDb()
        {
            var calculations = await GetAllCalculations();
            if (calculations is null || calculations.Count == 0)
                return 0;

            // Hämta vilka IDs som redan finns
            var ids = calculations.Select(c => c.SvSpelInfoId).ToList();

            var existingIds = await _db.Calculations
                .Where(c => ids.Contains(c.SvSpelInfoId))
                .Select(c => c.SvSpelInfoId)
                .ToListAsync();

            var toInsert = calculations
                .Where(c => !existingIds.Contains(c.SvSpelInfoId))
                .ToList();

            if (toInsert.Count == 0)
                return 0;

            await _db.Calculations.AddRangeAsync(toInsert);
            await _db.SaveChangesAsync();

            return toInsert.Count;
        }

    }
}

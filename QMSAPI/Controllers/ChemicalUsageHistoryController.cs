using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Data;
using QMSAPI.Dtos.ChemicalUsageHistor;
using QMSAPI.Models;

namespace QMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChemicalUsageHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChemicalUsageHistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChemicalUsageHistory>>> GetChemicalUsageHistory()
        {
            var histories = await _context.ChemicalUsageHistory
                .OrderByDescending(h => h.CTimestamp)
                .ToListAsync();

            return Ok(histories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChemicalUsageHistoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var history = new ChemicalUsageHistory
            {
                ChemicalId = dto.ChemicalId,
                ChemicalName = dto.ChemicalName,
                Action = dto.Action,
                PoolId = dto.PoolId,
                PoolName = dto.PoolName,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                AdjustedBy = dto.AdjustedBy,
                CTimestamp = DateTime.UtcNow,
                Note = dto.Note
            };

            _context.ChemicalUsageHistory.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = history.HistoryID }, history);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var history = await _context.ChemicalUsageHistory.FindAsync(id);
            if (history == null)
                return NotFound();

            return Ok(history);
        }
    }
}

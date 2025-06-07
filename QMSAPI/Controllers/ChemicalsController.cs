using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Data;
using QMSAPI.Dtos.Chemical;
using QMSAPI.Models;
using System.Linq;

namespace QMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChemicalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChemicalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/chemicals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chemical>>> GetChemicals()
        {
            return await _context.Chemicals.ToListAsync();
        }

        // POST: api/chemicals
        [HttpPost]
        public async Task<ActionResult<Chemical>> CreateChemical([FromBody] Chemical chemical)
        {
            if (chemical == null)
            {
                return BadRequest("Chemical data is null.");
            }

            _context.Chemicals.Add(chemical);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChemicalById), new { id = chemical.ChemicalId }, chemical);
        }
        // GET: api/chemicals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chemical>> GetChemicalById(int id)
        {
            var chemical = await _context.Chemicals.FindAsync(id);

            if (chemical == null)
            {
                return NotFound();
            }

            return chemical;
        }

        [HttpPatch("batch/applychemical")]
        public async Task<IActionResult> BatchAddQuantity([FromBody] BatchChemicalUpdateDto request)
        {
            if (request?.Chemicals == null || !request.Chemicals.Any())
            {
                return BadRequest("No chemicals provided.");
            }

            var ids = request.Chemicals.Select(c => c.ChemicalId).ToList();
            var chemicalsInDb = await _context.Chemicals
                .Where(c => ids.Contains(c.ChemicalId))
                .ToListAsync();

            foreach (var item in request.Chemicals)
            {
                var chemical = chemicalsInDb.FirstOrDefault(c => c.ChemicalId == item.ChemicalId);
                if (chemical != null)
                {
                    chemical.Quantity -= item.Quantity; // ✅ Cộng thêm số lượng
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Updated quantities successfully.",
                updatedCount = chemicalsInDb.Count
            });
        }

        [HttpPost("{id}/restock")]
        public async Task<IActionResult> AddQuantity(int id, [FromBody] UpdateChemicalDto dto)
        {
            var chemical = await _context.Chemicals.FindAsync(id);
            if (chemical == null)
            {
                return NotFound($"Chemical with ID {id} not found.");
            }

            if (dto.Quantity <= 0)
            {
                return BadRequest("Quantity to add must be greater than 0.");
            }

            // Cộng số lượng nhập kho
            chemical.Quantity += dto.Quantity;
            _context.Chemicals.Update(chemical);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                chemical.ChemicalId,
                chemical.ChemicalName,
                UpdatedQuantity = chemical.Quantity,
                Added = dto.Quantity,
                chemical.Unit
            });
        }
    }
}

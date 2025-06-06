using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using QMSAPI.Data;
using QMSAPI.Models;

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
    }
}

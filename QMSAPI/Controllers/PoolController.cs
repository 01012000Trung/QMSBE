using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure this is included
using QMSAPI.Data;

namespace QMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoolsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PoolsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pools
        [HttpGet]
        public async Task<IActionResult> GetPools()
        {
            var pools = await _context.Pools.ToListAsync(); // Corrected line
            return Ok(pools);
        }
    }
}
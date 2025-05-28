using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure this is included
using QMSAPI.Data;
using QMSAPI.Dtos.Pools;
using QMSAPI.Dtos.Staff;
using QMSAPI.Models;

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

        // POST: api/pools
        [HttpPost]
        public async Task<IActionResult> CreatePool([FromBody] PoolCreateDto dto)
        {
            var newPool = new Pools
            {
                PoolName = dto.PoolName,
                Size = dto.Size,
                MaxCapacity = dto.MaxCapacity,
                Depth = dto.Depth,
                PLocation = dto.PLocation,
                PStatus = dto.PStatus,
            };

            _context.Pools.Add(newPool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPoolById), new { id = newPool.PoolsId }, newPool);
        }

        // Optional: GET api/pools/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoolById(int id)
        {
            var pool = await _context.Pools.FindAsync(id);
            if (pool == null) return NotFound();
            return Ok(pool);
        }

        // DELETE: api/pools/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePool(int id)
        {
            var pool = await _context.Pools.FindAsync(id);
            if (pool == null)
            {
                return NotFound(new { message = $"Pool with ID = {id} not found." });
            }

            _context.Pools.Remove(pool);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Pool with ID = {id} deleted successfully." });
        }

        //PUT: api/pools/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePool(int id, [FromBody] PoolUpdateDto dto)
        {
            var pool = await _context.Pools.FindAsync(id);
            if (pool == null)
            {
                return NotFound(new { message = $"Pool with ID = {id} not found." });
            }

            pool.PoolName = dto.PoolName;
            pool.Size = dto.Size;
            pool.MaxCapacity = dto.MaxCapacity;
            pool.Depth = dto.Depth;
            pool.PLocation = dto.PLocation;
            pool.PStatus = dto.PStatus;

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Pool with ID = {id} updated successfully.", pool });
        }
    }
}
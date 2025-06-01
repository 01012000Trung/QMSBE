using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Data;
using QMSAPI.Dtos.WaterQualityParameter;
using QMSAPI.Models;

namespace QMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterQualityParametersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WaterQualityParametersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WaterQualityParameters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaterQualityParameterDto>>> GetAll()
        {
            var fullName = await GetCurrentUserFullNameAsync();
            if (string.IsNullOrEmpty(fullName))
                return Unauthorized();

            var parameters = await _context.WaterQualityParameters
                .Include(p => p.CreatedBy)
                .Where(p => p.CreatedBy.FullName == fullName)
                .Select(p => new WaterQualityParameterDto
                {
                    ParameterId = p.ParameterId,
                    PoolName = p.PoolName,
                    PTimestamp = p.PTimestamp,
                    TemperatureC = p.TemperatureC,
                    pHLevel = p.pHLevel,
                    ChlorineMgPerL = p.ChlorineMgPerL,
                    Notes = p.Notes,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy != null ? p.CreatedBy.FullName : null,
                    Status = p.Status,
                    Resolved = p.Resolved,
                    NeedsAction = p.NeedsAction
                })
                .ToListAsync();

            return Ok(parameters);
        }

        private async Task<string?> GetCurrentUserFullNameAsync()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return null;

            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);
            return staff?.FullName;
        }

        [HttpGet("me")]
        public async Task<ActionResult<IEnumerable<WaterQualityParameterDto>>> GetByCurrentUser()
        {
            var fullName = await GetCurrentUserFullNameAsync();
            if (string.IsNullOrEmpty(fullName))
                return Unauthorized();

            var parameters = await _context.WaterQualityParameters
                .Include(p => p.CreatedBy)
                .Where(p => p.CreatedBy.FullName == fullName) // 👈 lọc theo FullName nếu muốn
                .Select(p => new WaterQualityParameterDto
                {
                    ParameterId = p.ParameterId,
                    PoolName = p.PoolName,
                    PTimestamp = p.PTimestamp,
                    TemperatureC = p.TemperatureC,
                    pHLevel = p.pHLevel,
                    ChlorineMgPerL = p.ChlorineMgPerL,
                    Notes = p.Notes,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy != null ? p.CreatedBy.FullName : null,
                    Status = p.Status,
                    Resolved = p.Resolved,
                    NeedsAction = p.NeedsAction
                })
                .ToListAsync();

            return Ok(parameters);
        }

    }
}
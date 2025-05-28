using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Data;
using QMSAPI.Dtos.Staff;

namespace QMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var staff = await _context.Staff
                .FirstOrDefaultAsync(s => s.Username == loginDto.username);

            if (staff == null || staff.SPassword != loginDto.password)
            {
                return Unauthorized("Invalid username or password.");
            }

            var response = new StaffResponseDto
            {
                StaffId = staff.StaffId,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                FullName = staff.FullName,
                SRole = staff.SRole,
                Username = staff.Username,
                SPassword = staff.SPassword,
                Access = staff.Access,
                SAddress = staff.SAddress
            };

            return Ok(response);
        }
    }
}

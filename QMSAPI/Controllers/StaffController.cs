// Controllers/StaffController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Models;
using QMSAPI.Data;

namespace QMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaff()
        {
            return await _context.Staff.ToListAsync();
        }

        // POST: api/Staff/CreateStaff
        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto dto)
        {
            if (_context.Staff.Any(s => s.Username == dto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var staff = new Staff
            {
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FullName,
                SRole = dto.SRole,
                Username = dto.Username,
                SPassword = HashPassword(dto.SPassword), // Optional: hash password
                Access = dto.Access,
                SAddress = dto.SAddress
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return Ok(staff);
        }

        private string HashPassword(string password)
        {
            // Implement password hashing here (e.g., using SHA256 or BCrypt)
            return password; // Temporary - you should hash the password in production
        }

    }
}

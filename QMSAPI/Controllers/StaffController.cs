// Controllers/StaffController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Data;
using QMSAPI.Dtos.Staff;
using QMSAPI.Models;

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

        // DELETE: api/staff/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound($"No staff found with ID = {id}");
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return Ok($"Staff with ID = {id} has been deleted.");
        }

        // PUT: api/staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffUpdateDto updateDto)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound($"No staff found with ID = {id}");
            }

            // Cập nhật các trường (bạn có thể kiểm tra null nếu muốn)
            staff.Email = updateDto.Email;
            staff.PhoneNumber = updateDto.PhoneNumber;
            staff.FullName = updateDto.FullName;
            staff.SRole = updateDto.SRole;
            staff.Username = updateDto.Username;
            staff.SPassword = updateDto.SPassword;
            staff.Access = updateDto.Access;
            staff.SAddress = updateDto.SAddress;

            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();

            return Ok(staff);
        }
    }
}

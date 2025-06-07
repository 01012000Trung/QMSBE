using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSAPI.Data;
using QMSAPI.Dtos.Staff;
using QMSAPI.Models;
using Microsoft.AspNetCore.Identity;

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
            return await _context.Staff
                .Select(s => new Staff // Ẩn mật khẩu khi trả về
                {
                    StaffId = s.StaffId,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    FullName = s.FullName,
                    SRole = s.SRole,
                    Username = s.Username,
                    Access = s.Access,
                    SAddress = s.SAddress
                })
                .ToListAsync();
        }

        // POST: api/Staff
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
                SPassword = HashPassword(dto.SPassword), // Mã hóa mật khẩu
                Access = dto.Access,
                SAddress = dto.SAddress
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            // Ẩn mật khẩu khi trả về
            staff.SPassword = null;
            return Ok(staff);
        }

        // DELETE: api/Staff/{id}
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

        // PUT: api/Staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffUpdateDto updateDto)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound($"No staff found with ID = {id}");
            }

            // Cập nhật thông tin
            staff.Email = updateDto.Email;
            staff.PhoneNumber = updateDto.PhoneNumber;
            staff.FullName = updateDto.FullName;
            staff.SRole = updateDto.SRole;
            staff.Username = updateDto.Username;
            staff.Access = updateDto.Access;
            staff.SAddress = updateDto.SAddress;

            // Chỉ mã hóa nếu mật khẩu được cung cấp
            if (!string.IsNullOrEmpty(updateDto.SPassword))
            {
                staff.SPassword = HashPassword(updateDto.SPassword);
            }

            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();

            // Ẩn mật khẩu khi trả về
            staff.SPassword = null;
            return Ok(staff);
        }

        // ✅ Hàm mã hóa mật khẩu
        private string HashPassword(string plainPassword)
        {
            var hasher = new PasswordHasher<Staff>();
            return hasher.HashPassword(null, plainPassword);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QMSAPI.Data;
using QMSAPI.Dtos.Login;
using QMSAPI.Dtos.Staff;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] QMSAPI.Dtos.Login.LoginDto loginDto)
        {
            var staff = await _context.Staff
                .FirstOrDefaultAsync(s => s.Username == loginDto.Username);

            if (staff == null || staff.SPassword != loginDto.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(staff);

            var response = new
            {
                Token = token,
                Staff = new StaffResponseDto
                {
                    StaffId = staff.StaffId,
                    Email = staff.Email,
                    PhoneNumber = staff.PhoneNumber,
                    FullName = staff.FullName,
                    SRole = staff.SRole,
                    Username = staff.Username,
                    Access = staff.Access,
                    SAddress = staff.SAddress
                }
            };

            return Ok(response);
        }

        // ✅ Generate JWT token
        private string GenerateJwtToken(Models.Staff staff)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, staff.StaffId.ToString()),
                    new Claim(ClaimTypes.Name, staff.Username),
                    new Claim(ClaimTypes.Role, staff.SRole ?? "User") // fallback if null
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

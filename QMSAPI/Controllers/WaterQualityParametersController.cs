﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll()
        {
            var parameters = await _context.WaterQualityParameters
                .OrderByDescending(p => p.PTimestamp) // Sắp xếp mới nhất trước
                .ToListAsync();

            return Ok(parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WaterQualityParameterCreateDto dto)
        {
            // Tìm Pool theo tên
            var pool = await _context.Pools.FirstOrDefaultAsync(p => p.PoolName == dto.PoolName);
            if (pool == null)
                return NotFound($"Không tìm thấy hồ bơi tên '{dto.PoolName}'.");

            var param = new WaterQualityParameter
            {
                PoolId = pool.PoolsId,
                PoolName = pool.PoolName, // <-- gán lại nếu muốn lưu trong bảng
                PTimestamp = dto.PTimestamp,
                TemperatureC = (float)dto.TemperatureC,
                pHLevel = (float)dto.pHLevel,
                ChlorineMgPerL = (float)dto.ChlorineMgPerL,
                Notes = dto.Notes,
                CreatedBy = dto.StaffId,
                RStatus = dto.RStatus,
                Resolved = dto.Resolved,
                NeedsAction = dto.NeedsAction
            };

            _context.WaterQualityParameters.Add(param);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = param.ParameterId }, param);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<WaterQualityParameter>> GetById(int id)
        {
            var param = await _context.WaterQualityParameters.FindAsync(id);

            if (param == null)
                return NotFound();

            return param;
        }

        [HttpPatch("{id}/resolve")]
        public async Task<IActionResult> UpdateResolved(int id, [FromBody] UpdateResolvedDto dto)
        {
            var parameter = await _context.WaterQualityParameters.FindAsync(id);
            if (parameter == null)
            {
                return NotFound(new { message = $"Parameter with ID {id} not found." });
            }

            parameter.Resolved = dto.Resolved;

            // Nếu Resolved = true thì tự động bỏ NeedsAction = false (tuỳ yêu cầu logic)
            if (dto.Resolved)
            {
                parameter.NeedsAction = false;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Resolved status updated successfully." });
        }

    }
}
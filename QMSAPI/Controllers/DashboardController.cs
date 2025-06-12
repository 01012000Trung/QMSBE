using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QMSAPI.Models;

namespace QMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // API: /api/Dashboard/summary
        [HttpGet("summary")]
        public async Task<ActionResult<DashBoardSummary>> DashBoardSummary()
        {
            var summary = new DashBoardSummary();

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    -- Pools summary
                    (SELECT COUNT(*) FROM Pools) AS TotalPools,
                    (SELECT COUNT(*) FROM Pools WHERE PStatus = 'active') AS ActivePools,
                    (SELECT COUNT(*) FROM Pools WHERE PStatus = 'maintenance') AS MaintenancePools,
                    (SELECT COUNT(*) FROM Pools WHERE PStatus = 'closed') AS ClosedPools,

                    -- Water quality alerts
                    (SELECT COUNT(*) FROM WaterQualityParameters 
                    WHERE RStatus IN ('Critical', 'Warning') AND Resolved = 0) AS TotalAlerts,

                    (SELECT COUNT(*) FROM WaterQualityParameters 
                     WHERE RStatus = 'Critical' AND Resolved = 0) AS CriticalAlerts,

                    (SELECT COUNT(*) FROM WaterQualityParameters 
                     WHERE RStatus = 'Warning' AND Resolved = 0) AS WarningAlerts,

                    -- Today's measurements
                    (SELECT COUNT(*) FROM WaterQualityParameters
                    WHERE CAST(PTimestamp AS DATE) = CAST(GETDATE() AS DATE) ) AS TotalMeasurementsToday;
            ", conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                summary.TotalPools = reader.GetInt32(0);
                                summary.ActivePools = reader.GetInt32(1);
                                summary.MaintenancePools = reader.GetInt32(2);
                                summary.ClosedPools = reader.GetInt32(3);
                                summary.TotalAlerts = reader.GetInt32(4);
                                summary.CriticalAlerts = reader.GetInt32(5);
                                summary.WarningAlerts = reader.GetInt32(6);
                                summary.TodayMeasurements = reader.GetInt32(7);
                            }
                        }
                    }
                }

                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
    }
}

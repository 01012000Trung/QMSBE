using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMSAPI.Models
{
    public class WaterQualityParameter
    {
        [Key]
        public int ParameterId { get; set; }
        public int PoolId { get; set; }

        public string PoolName { get; set; } = string.Empty;

        public DateTime PTimestamp { get; set; }

        public double TemperatureC { get; set; }

        public double pHLevel { get; set; }

        public double ChlorineMgPerL { get; set; }

        public string? Notes { get; set; }

        public string? RStatus { get; set; }

        public bool Resolved { get; set; } = false;

        public bool NeedsAction { get; set; } = true;

        public int CreatedBy { get; set; }
    }
}

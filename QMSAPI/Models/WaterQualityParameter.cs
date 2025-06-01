using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMSAPI.Models
{
    public class WaterQualityParameter
    {
        [Key]
        public int ParameterId { get; set; }

        public string PoolName { get; set; } = string.Empty;

        public DateTime PTimestamp { get; set; }

        public float TemperatureC { get; set; }

        public float pHLevel { get; set; }

        public float ChlorineMgPerL { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? CreatedById { get; set; }

        public string? Status { get; set; }

        public bool Resolved { get; set; } = false;

        public bool NeedsAction { get; set; } = true;

        public Staff? CreatedBy { get; set; }
    }
}

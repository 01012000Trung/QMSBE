using System.Text.Json.Serialization;

namespace QMSAPI.Dtos.WaterQualityParameter
{
    public class WaterQualityParameterCreateDto
    {
        public string PoolName { get; set; } = string.Empty;
        public DateTime PTimestamp { get; set; }
        public float TemperatureC { get; set; }
        public float pHLevel { get; set; }
        public float ChlorineMgPerL { get; set; }
        public string? Notes { get; set; }

        [JsonPropertyName("createdBy")]
        public int StaffId { get; set; }
        public string? RStatus { get; set; }
        public bool Resolved { get; set; } = false;
        public bool NeedsAction { get; set; } = true;
    }
}
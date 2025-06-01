namespace QMSAPI.Dtos.WaterQualityParameter
{
    public class WaterQualityParameterCreateDto
    {
        public int PoolId { get; set; }
        public string PoolName { get; set; } = string.Empty;
        public DateTime PTimestamp { get; set; }
        public double TemperatureC { get; set; }
        public double pHLevel { get; set; }
        public double ChlorineMgPerL { get; set; }
        public string? Notes { get; set; }
        public int? CreatedById { get; set; }
        public string? Status { get; set; }
        public bool Resolved { get; set; } = false;
        public bool NeedsAction { get; set; } = true;
    }
}
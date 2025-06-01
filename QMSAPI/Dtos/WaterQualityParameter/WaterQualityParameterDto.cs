namespace QMSAPI.Dtos.WaterQualityParameter
{
    public class WaterQualityParameterDto
    {
        public int ParameterId { get; set; }
        public string PoolName { get; set; }
        public DateTime PTimestamp { get; set; }
        public double TemperatureC { get; set; }
        public double pHLevel { get; set; }
        public double ChlorineMgPerL { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public bool Resolved { get; set; }
        public bool NeedsAction { get; set; }
    }
}

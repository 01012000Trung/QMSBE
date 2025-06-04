namespace QMSAPI.Dtos.WaterQualityParameter
{
    public class WaterQualityParameterDto
    {
        public int ParameterId { get; set; }
        public string PoolName { get; set; }
        public DateTime PTimestamp { get; set; }
        public float TemperatureC { get; set; }
        public float pHLevel { get; set; }
        public float ChlorineMgPerL { get; set; }
        public string? Notes { get; set; }
        public int CreatedBy { get; set; }
        public string? RStatus { get; set; }
        public bool Resolved { get; set; }
        public bool NeedsAction { get; set; }
    }
}

namespace QMSAPI.Dtos.Pools
{
    public class PoolUpdateDto
    {
        public string PoolName { get; set; }
        public double Size { get; set; }
        public int MaxCapacity { get; set; }
        public double Depth { get; set; }
        public string PLocation { get; set; }
        public string PStatus { get; set; }
    }
}

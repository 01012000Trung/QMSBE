using System.ComponentModel.DataAnnotations;
namespace QMSAPI.Models
{
    public class Pools
{
    public int PoolsId { get; set; }
    public string PoolName { get; set; }
    public double Size { get; set; }
    public int MaxCapacity { get; set; }
    public double Depth { get; set; }
    public string PLocation { get; set; }
    public string PStatus { get; set; }
    public DateTime? LastCleaningDate { get; set; }
}
}

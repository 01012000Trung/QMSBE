using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMSAPI.Models
{
    public class ChemicalUsageHistory
    {
        [Key]
        public int HistoryID { get; set; }

        public int ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public string Action { get; set; }
        public int PoolId { get; set; }
        public string PoolName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }

        public int? AdjustedBy { get; set; }

        [ForeignKey("AdjustedBy")]
        public Staff Staff { get; set; }  // Navigation property FK

        public string CStatus { get; set; }
        public DateTime CTimestamp { get; set; }
        public string Note { get; set; }
    }
}

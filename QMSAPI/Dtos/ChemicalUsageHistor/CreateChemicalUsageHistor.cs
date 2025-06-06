using QMSAPI.Models;

namespace QMSAPI.Dtos.ChemicalUsageHistor
{
    public class CreateChemicalUsageHistoryDto
    {
        public int ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public string Action { get; set; }
        public int PoolId { get; set; }
        public string PoolName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }

        public int? AdjustedBy { get; set; }  // Chỉ giữ ID thôi

        public string CStatus { get; set; }
        public string Note { get; set; }
    }
}
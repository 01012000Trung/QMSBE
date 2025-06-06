namespace QMSAPI.Models
{
    public class Chemical
    {
        public int ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public string ChemicalType { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double MinThreshold { get; set; }
        public double ReorderLevel { get; set; }
        public string? ChDescription { get; set; }
    }
}

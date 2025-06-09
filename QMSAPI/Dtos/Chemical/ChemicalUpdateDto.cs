namespace QMSAPI.Dtos.Chemical
{
    public class ChemicalUpdateDto
    {
        public string ChemicalName { get; set; }
        public string ChemicalType { get; set; }
        public float MinThreshold { get; set; }
        public float ReorderLevel { get; set; }
        public string Unit { get; set; }
        public string ChDescription { get; set; }
    }
}

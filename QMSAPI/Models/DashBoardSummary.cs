namespace QMSAPI.Models
{
    public class DashBoardSummary
    {
        public int TotalPools { get ; set; }
        public int ActivePools { get; set; }
        public int MaintenancePools { get; set; }
        public int ClosedPools { get; set; }

        public int TotalAlerts { get; set; }         // Tổng số cảnh báo (Warning + Critical)
        public int CriticalAlerts { get; set; }      // Cảnh báo nghiêm trọng
        public int WarningAlerts { get; set; }

        public int TodayMeasurements { get; set; }
    }
}

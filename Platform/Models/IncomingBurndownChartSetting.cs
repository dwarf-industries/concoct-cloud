namespace Platform.Models
{
    public class IncomingBurndownChartSetting
    {
        public int ProjectId { get; set; }
        public int Dashboard { get; set; }
        public int ViewComponentId { get; set; }
        public string Title { get; set; }
        public int BacklogBindingType { get; set; }
        public int BacklogSelectedType { get; set; }
        public int WorkItemTypeSelected { get; set; }
        public int CountWItemSelected { get; set; }
        public int SumWItemSelected { get; set; }
    }
}
namespace Platform.Models
{
    public class IncomingWidgetCreatorRequest
    {

        public string ControlName { get; set; }
        public string TableName { get; set; }
        public ConditionalRule rule { get; set; }
        public int NewRow { get; set; }
        public string Column { get; set; }
        public string BindingRow { get; set; }
        public int Index { get; set; }
      
    }
}
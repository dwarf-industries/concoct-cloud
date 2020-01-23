namespace RokonoControl.Models
{
    public class OutgoingBindingWorkItem
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string ItemType { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemState { get; set; }
        public int ItemStateId { get; set; }
    }
}
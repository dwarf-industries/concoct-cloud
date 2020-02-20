namespace RokonoControl.Models
{
    using System.Collections.Generic;
    public class CommitFileHirarhicalData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullPathName { get; set; }
        public int ItemId { get; set; }
        public int InternalId { get; set; }
        public List<CommitFileHirarhicalData> SubChild { get; set; }
    }
}
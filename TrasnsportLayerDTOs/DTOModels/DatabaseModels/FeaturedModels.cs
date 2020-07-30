using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class FeaturedModels
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Paragraph1 { get; set; }
        public string SubTitle { get; set; }
        public string Paragraph2 { get; set; }
        public string ModelPath { get; set; }
        public string ModelName { get; set; }
        public int? IsFeatured { get; set; }
    }
}

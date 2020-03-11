namespace Platform.Models
{
    public class IncomingNoteRequest
    {
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public string Background { get; set; }
        public string FontColor { get; set; }
    }
}
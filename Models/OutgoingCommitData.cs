using System;

namespace RokonoControl.Models
{
    public class OutgoingCommitData
    {
        public string Author { get; set; }
        public string CommitKey { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string PullRequest { get; set; }
    }
}
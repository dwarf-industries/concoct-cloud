using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class Boards
    {
        public Boards()
        {
            AssociatedBoardWorkItems = new HashSet<AssociatedBoardWorkItems>();
            AssociatedProjectBoards = new HashSet<AssociatedProjectBoards>();
        }

        public int Id { get; set; }
        public int RepositoryId { get; set; }
        public int BoardType { get; set; }
        public string BoardName { get; set; }

        public virtual ICollection<AssociatedBoardWorkItems> AssociatedBoardWorkItems { get; set; }
        public virtual ICollection<AssociatedProjectBoards> AssociatedProjectBoards { get; set; }
    }
}

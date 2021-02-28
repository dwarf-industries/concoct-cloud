using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectBoards
    {
        public int Id { get; set; }
        public int? BoardId { get; set; }
        public int? ProjectId { get; set; }
        public int? Position { get; set; }

        public virtual Boards Board { get; set; }
        public virtual Projects Project { get; set; }
    }
}

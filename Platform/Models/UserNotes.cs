using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class UserNotes
    {
        public UserNotes()
        {
            AssociatedAccountNotes = new HashSet<AssociatedAccountNotes>();
        }

        public int Id { get; set; }
        public string NoteBackground { get; set; }
        public string NoteForeground { get; set; }
        public string Content { get; set; }
        public DateTime? DateOfMessage { get; set; }
        public string TopPos { get; set; }
        public string LeftPos { get; set; }

        public virtual ICollection<AssociatedAccountNotes> AssociatedAccountNotes { get; set; }
    }
}

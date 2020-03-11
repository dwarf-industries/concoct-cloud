using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedAccountNotes
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int UserAccountId { get; set; }
        public int ProjectId { get; set; }

        public virtual UserNotes Note { get; set; }
        public virtual Projects Project { get; set; }
        public virtual UserAccounts UserAccount { get; set; }
    }
}

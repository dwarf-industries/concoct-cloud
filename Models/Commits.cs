﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class Commits
    {
        public Commits()
        {
            AssociatedBranchCommits = new HashSet<AssociatedBranchCommits>();
            AssociatedCommitFiles = new HashSet<AssociatedCommitFiles>();
        }

        public int Id { get; set; }
        public string CommitData { get; set; }
        public DateTime? DateOfCommit { get; set; }
        public string CommitedBy { get; set; }
        public string CommitKey { get; set; }

        public virtual ICollection<AssociatedBranchCommits> AssociatedBranchCommits { get; set; }
        public virtual ICollection<AssociatedCommitFiles> AssociatedCommitFiles { get; set; }
    }
}
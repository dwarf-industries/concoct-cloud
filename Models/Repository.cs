﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class Repository
    {
        public Repository()
        {
            AssociatedProjectBuilds = new HashSet<AssociatedProjectBuilds>();
            AssociatedProjectMembers = new HashSet<AssociatedProjectMembers>();
            AssociatedRepositoryBranches = new HashSet<AssociatedRepositoryBranches>();
            Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }
        public string FolderPath { get; set; }
        public string RepositoryLocation { get; set; }
        public string LinuxFolderPath { get; set; }

        public virtual ICollection<AssociatedProjectBuilds> AssociatedProjectBuilds { get; set; }
        public virtual ICollection<AssociatedProjectMembers> AssociatedProjectMembers { get; set; }
        public virtual ICollection<AssociatedRepositoryBranches> AssociatedRepositoryBranches { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}

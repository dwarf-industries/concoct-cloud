﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class WorkItemIterations
    {
        public WorkItemIterations()
        {
            AssociatedProjectIterations = new HashSet<AssociatedProjectIterations>();
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string IterationName { get; set; }
        public int? IsActive { get; set; }
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }

        public virtual ICollection<AssociatedProjectIterations> AssociatedProjectIterations { get; set; }
        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class Tags
    {
        public Tags()
        {
            AssociatedWorkItemTags = new HashSet<AssociatedWorkItemTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AssociatedWorkItemTags> AssociatedWorkItemTags { get; set; }
    }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedWorkItemMessages
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public int MessageId { get; set; }

        public virtual WorkItemMessage Message { get; set; }
        public virtual WorkItem WorkItem { get; set; }
    }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class SurveyComponent
    {
        public SurveyComponent()
        {
            AssociatedPageSurveyComponents = new HashSet<AssociatedPageSurveyComponents>();
        }

        public int Id { get; set; }
        public string ComponentInternalName { get; set; }
        public string PlatformName { get; set; }

        public virtual ICollection<AssociatedPageSurveyComponents> AssociatedPageSurveyComponents { get; set; }
    }
}
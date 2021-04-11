using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class SurveyPage
    {
        public SurveyPage()
        {
            AssociatedPageSurveyComponents = new HashSet<AssociatedPageSurveyComponents>();
        }

        public int Id { get; set; }
        public string PageName { get; set; }
        public int? SurveyId { get; set; }

        public virtual Surveys Survey { get; set; }
        public virtual ICollection<AssociatedPageSurveyComponents> AssociatedPageSurveyComponents { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Surveys
    {
        public Surveys()
        {
            SurveyPage = new HashSet<SurveyPage>();
        }

        public int Id { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyLogo { get; set; }
        public string SurveyDescription { get; set; }
        public string Email { get; set; }
        public int? ProjectId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual UserAccounts CreatedByNavigation { get; set; }
        public virtual Projects Project { get; set; }
        public virtual ICollection<SurveyPage> SurveyPage { get; set; }
    }
}

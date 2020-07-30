using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class LandingPage
    {
        public int Id { get; set; }
        public string BannerImage { get; set; }
        public string BannerHeading { get; set; }
        public string BannerParagraph { get; set; }
        public string SecondBannerImage { get; set; }
        public string SecondBannerHeading { get; set; }
        public string EndDate { get; set; }
        public decimal? TargetFunding { get; set; }
        public decimal? CurrentMonthFunding { get; set; }
        public string FundingHeading { get; set; }
        public string FundingParagraph { get; set; }
        public string FacebookLink { get; set; }
        public string TwitchLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
    }
}

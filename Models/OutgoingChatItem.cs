using System.Collections.Generic;

namespace Platform.Models
{
    public class OutgoingChatItem
    {
        //{ nodeId: '01-01', nodeText: 'Home', iconCss: 'icon-circle-thin icon', link:'/Dashboard/ProjectDashboard?id=@ProjectId' },
        public int InternalId {get; set;}
        public string NodeText { get; set; }
        public string IconCss { get; set; }
        public string Link { get; set; }
        public bool IsParent { get; set; }
        public int ChannelType { get; set; }
        public int IsExpand { get; set; }
        public int ParentId {get; set;}
        public List<OutgoingChatItemChild> NodeChild { get; set; }
    }
}
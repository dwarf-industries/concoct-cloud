
namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;
    [ViewComponent(Name = "ChatRoomSetting")]

    public class ChatRoomSettingViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ChatRoomSettingViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ChatRoomId"] = request.Id;
            ViewData["ProjectId"] = request.ProjectId;
            return View("/Views/Shared/Components/ChatComponents/ChatRoomSetting/Default.cshtml");
        }
    }
}
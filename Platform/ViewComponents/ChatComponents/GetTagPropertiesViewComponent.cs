

namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    [ViewComponent(Name = "GetTagProperties")]
    public class GetTagPropertiesViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
        public GetTagPropertiesViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["ChatRoom"] = request.UserId;
            ViewData["FormOption"] = request.WorkItemType;
            using(var context = new ChatContext(Context,Configuration))
            {
                ViewData["Tag"] = context.GetChatRightById(request.Id);
            }
            return View("/Views/Shared/Components/ChatComponents/GetTagProperties/Default.cshtml");
        }
    }
}
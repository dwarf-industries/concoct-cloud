namespace Rokono_Control.ViewComponents.Widgets
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    [ViewComponent(Name = "GetDiscussionBoard")]
    public class DisucssionViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private AutherizationManager AutherizationManager;
        private int UserId;

        public DisucssionViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            if (request == null)
                return View("/Views/Shared/Components/Discussion/Default.cshtml");

            using (var context = new ChatContext(Context, Configuration))
            {
                switch(request.WorkItemType)
                {
                    case 1:
                        ViewData["DiscussionMessages"] = context.GetWorkItemDiscussions(request);
                        break;
                }
            }
            return View("/Views/Shared/Components/Discussion/Default.cshtml");
        }
    }
}

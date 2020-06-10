namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;
    [ViewComponent(Name = "ChatSettings")]

    public class ChatSettingsViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        private  AutherizationManager AutherizationManager;

        private int UserId;
 
        public ChatSettingsViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
    
            ViewData["User"] = UserId;
            ViewData["projectId"] = request.ProjectId;
            ViewData["DefaultChatRoom"] = request.Id;
            if(request.Id != 0)
                return View();

            using(var context = new ChatContext(Context, Configuration))
            {
                ViewData["DefaultChatRoom"] = context.GetDefaultProjectChatRoom(request.ProjectId);
            }
            return View("/Views/Shared/Components/ChatComponents/ChatSettings/Default.cshtml");
        }
    }
}
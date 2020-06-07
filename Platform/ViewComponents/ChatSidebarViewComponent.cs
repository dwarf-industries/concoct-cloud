namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class ChatSidebarViewComponent : ViewComponent 
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public ChatSidebarViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }


        public IViewComponentResult Invoke(int projectId)
        {

            using(var context = new DatabaseController(Context,Configuration))
            {
                var currentUser = context.GetUserAccount(UserId);
                ViewData["Username"] = currentUser.Email;
                ViewData["ProjectId"] = projectId;
                ViewData["GetDefaultActiveRoom"] = context.GetDefaultProjectChannel(projectId); 
                ViewData["ProjectChatRights"] = context.GetProjectChatRights(projectId);
                ViewData["GetChatRights"] = context.GetUserRights(UserId,projectId);
            }
            return View();
        }
    }
}
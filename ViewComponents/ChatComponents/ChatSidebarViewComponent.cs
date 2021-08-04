namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    [ViewComponent(Name = "ChatSidebar")]

    public class ChatSidebarViewComponent : ViewComponent 
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public ChatSidebarViewComponent(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }


        public IViewComponentResult Invoke(int projectId)
        {
            var currentUser = default(UserAccounts);
            using(var context = new UsersContext(Context,Configuration))
            {
                currentUser = context.GetUserAccount(UserId);
                ViewData["GetChatRights"] = context.GetUserRights(UserId,projectId);
            }
            using(var context = new ChatContext(Context,Configuration))
            {
                 ViewData["Username"] = currentUser.Email;
                ViewData["ProjectId"] = projectId;
                ViewData["GetDefaultActiveRoom"] = context.GetDefaultProjectChannel(projectId); 
                ViewData["ProjectChatRights"] = context.GetProjectChatRights(projectId);
            }
            return View("/Views/Shared/Components/ChatComponents/ChatSidebar/Default.cshtml");
        }
    }
}
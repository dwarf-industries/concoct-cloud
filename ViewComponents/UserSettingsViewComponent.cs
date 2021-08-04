namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class UserSettingsViewComponent : ViewComponent
    {
        
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public UserSettingsViewComponent(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            using(var context = new UsersContext(Context,Configuration))
                ViewData["UserData"] = context.GetUserAccount(UserId);
            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["AssignedWorkItemCount"] = context.GetWorkItemsCountForUser(UserId);
            using (var context = new NotificationContext(Context, Configuration))
                ViewData["Notifications"] = context.GetAllUserNotifications(UserId, projectId).Count;
            return View();
        }
    }
}
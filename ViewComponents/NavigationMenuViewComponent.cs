namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public NavigationMenuViewComponent(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(int projectId)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {   
                ViewData["ProjectId"] = projectId;
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["DefaultIteration"] = context.GetProjectDefautIteration(projectId);
            }
             using(var context = new UsersContext(Context,Configuration))
            {
                ViewData["Name"] = context.GetUsername(UserId);
                ViewData["Projects"] = context.GetUserProjects(UserId);
                ViewData["DefaultDashboard"] = context.GetUserDefaultDashboard(UserId, projectId);
            }
            return View();
        }
    }
}
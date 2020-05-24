namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class ChatSidebarViewComponent : ViewComponent 
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ChatSidebarViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                var currentUser = context.GetUserAccount(Id);
                ViewData["Username"] = currentUser.Email;
                ViewData["ProjectId"] = projectId;
                ViewData["GetDefaultActiveRoom"] = 1;

            }
            return View();
        }
    }
}
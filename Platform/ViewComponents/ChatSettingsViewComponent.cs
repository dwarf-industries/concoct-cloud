using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class ChatSettingsViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ChatSettingsViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            ViewData["User"] = Id;
            ViewData["projectId"] = request.ProjectId;
            ViewData["DefaultChatRoom"] = request.Id;
            if(request.Id != 0)
                return View();

            using(var context = new DatabaseController(Context, Configuration))
            {
                ViewData["DefaultChatRoom"] = context.GetDefaultProjectChatRoom(request.ProjectId);
            }
            return View();
        }
    }
}
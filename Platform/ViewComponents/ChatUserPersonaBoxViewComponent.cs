
namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class ChatUserPersonaBoxViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ChatUserPersonaBoxViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                var rights = context.GetUserRights(request.Id, request.ProjectId);
                if(rights != null)
                    ViewData["UserAdministrativeRights"] = rights.ChatChannelsRule == 1 ? true :false;
                ViewData["UserId"] = request.Id;
            }
            return View();
        }
    }
}
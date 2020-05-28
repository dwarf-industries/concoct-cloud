

namespace Platform.ViewComponents
{

    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    public class GetTagPropertiesViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public GetTagPropertiesViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["ChatRoom"] = request.UserId;
            ViewData["FormOption"] = request.WorkItemType;
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["Tag"] = context.GetChatRightById(request.Id);
            }
            return View();
        }
    }
}

namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class ChatWIndowViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ChatWIndowViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            ViewData["TransferId"] = request.Id;
            ViewData["ProjectId"] = request.ProjectId;
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ChatMessages"] = context.GetCannelMessages(request.Id);
            }
            return View();
        }
    }
}
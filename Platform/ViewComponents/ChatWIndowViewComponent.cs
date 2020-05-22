
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

        public IViewComponentResult Invoke(int chatroomId)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["TransferId"] = chatroomId;
            }
            return View();
        }
    }
}
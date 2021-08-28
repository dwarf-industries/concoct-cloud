namespace Rokono_Control.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Hubs;
    using Rokono_Control.DatabaseHandlers.Contexts;
    using Rokono_Control.Models;

    public class BlogController : Controller
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        IHubContext<MessageHub> MessageContext;

        AutherizationManager AutherizationManager { get; set; }
        private int UserId { get; set; }


        public BlogController(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor, IHubContext<MessageHub> hubContext)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
            MessageContext = hubContext;
        }

        public IActionResult Index(int projectId)
        {
            using (var context = new BlogContext(Context, Configuration))
                ViewData["BlogCategories"] = context.GetBlogCategories(projectId);

            return View();
        }

    }
}



namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DocumentationNavigation")]
    public class DocumentationNavigationViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public DocumentationNavigationViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        public IViewComponentResult Invoke(int id)
        {
            ViewData["ProjectId"] = id;
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var currentUser = int.Parse(user.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["UserRights"] = context.GetUserRights(currentUser,id);
            }
            return View("/Views/Shared/Components/Documentation/DocumentationNavigation/Default.cshtml");
        }
    }
}
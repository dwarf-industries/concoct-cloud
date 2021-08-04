
namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;

    public class RelateWorkItemViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public RelateWorkItemViewComponent(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest ids)
        {
            ViewData["WorkItemId"] = ids.Id;
            ViewData["ProjectsId"] = ids.WorkItemType;
            return View();
        }
    }
}
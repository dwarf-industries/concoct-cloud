namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Rokono_Control.Models;

    public class SearchBoxViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public SearchBoxViewComponent(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            
            using(var context = new WorkItemsContext(Context, Configuration))
            {
                ViewData["CurrentIteration"] = context.GetProjectDefautIteration(projectId);
            }
            return View();
        }
    }
}
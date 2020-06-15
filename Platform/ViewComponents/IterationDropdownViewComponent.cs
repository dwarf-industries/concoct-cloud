namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Rokono_Control.Models;

    public class IterationDropdownViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public IterationDropdownViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest ids)
        {
            ViewData["ProjectId"] = ids.ProjectId;
            ViewData["Calling"]= ids.Phase;
            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["CurrentIteration"] =  context.GetProjectIteration(ids.Id);
            return View();
        }
        
    }
}
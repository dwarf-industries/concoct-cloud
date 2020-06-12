namespace Platform.ViewComponents
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Rokono_Control.Models;

    public class IterationManagerViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public IterationManagerViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["CurrentIteration"] = request.Id;
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                ViewData["ProjectIterations"] = context.GetProjectIterations(request.ProjectId); 
            }
            return View();
        }
    }
}
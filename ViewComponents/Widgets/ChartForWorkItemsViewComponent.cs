namespace Platform.ViewComponents.Widgets
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    
    [ViewComponent(Name = "ChartForWorkItems")]
    public class ChartForWorkItemsViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ChartForWorkItemsViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["DashboardId"] = request.Id;
            ViewData["ContainerId"] = request.WorkItemType;
         
            return View("/Views/Shared/Components/Widgets/ChartForWorkItems/Default.cshtml");
        }
    }
}
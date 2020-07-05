using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.Models;

namespace Platform.ViewComponents.Widgets
{        
    [ViewComponent(Name = "BurdownChart")]
    public class BurdownChartViewComponent : ViewComponent
    {
        
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public BurdownChartViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["DashboardId"] = request.Id;
            ViewData["Height"] = request.Phase;

            return View("/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml");
        }
    }
}
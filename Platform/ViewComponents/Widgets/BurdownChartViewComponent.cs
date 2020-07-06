using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.Models;
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
            var bindingData = default(IncomingBurndownChartSetting);
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["DashboardId"] = request.Id;
            ViewData["Height"] = request.Phase;
            ViewData["ContainerId"] = request.WorkItemType;
            if(!string.IsNullOrEmpty(request.Phase))
             bindingData = JsonConvert.DeserializeObject<IncomingBurndownChartSetting>(request.Phase);
            if(bindingData != null)
                ViewData["ChartBindingData"] = bindingData; 
            return View("/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml");
        }
    }
}

namespace Platform.ViewComponents.Widgets
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DataBinder")]
    public class DataBinderViewComponent : ViewComponent
    {
        
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public DataBinderViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View("/Views/Shared/Components/Widgets/DataBinder/Default.cshtml");
        }
    }
}
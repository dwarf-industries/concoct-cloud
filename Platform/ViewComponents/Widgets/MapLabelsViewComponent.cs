namespace Platform.ViewComponents.Widgets
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;

    [ViewComponent(Name = "MapLabels")]
    public class MapLabelsViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public MapLabelsViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke()
        {
            
            return View("/Views/Shared/Components/Widgets/MapLabels/Default.cshtml");
        }
    }
}